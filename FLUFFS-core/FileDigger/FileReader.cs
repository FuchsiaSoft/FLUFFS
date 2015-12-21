using FileDigger.Properties;
using Hasher;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using OdfDigger;
using System;
using System.Text;
using Pri.LongPath;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BinaryDigger;
using OpenSDKDigger;

namespace FileDigger
{
    public class FileReader : IFileReader
    {
        private string _InternalFilePath;

        private byte[] _InternalBuffer = null;

        private const string UNREADABLE_FILE_MESSAGE =
            "The specified file is not supported.";

        private const string NOT_INITIALISED_MESSAGE =
            "The FileReader has not been initialised correctly, " +
            "either call with a file path in the constructor or " +
            "call the Open method";

        public FileReader() { }

        public FileReader(string path)
        {
            Open(path);
        }

        public void Open(string path)
        {
            if (IsReadable(path) == false)
            {
                throw new System.IO.InvalidDataException(UNREADABLE_FILE_MESSAGE);
            }

            if (Settings.Default.MakeLocalCache)
            {
                string tempPath = Path.GetTempFileName();

                //lots of checks are done on file extension, so append
                //the exiting file extension.
                tempPath = Path.ChangeExtension(tempPath, Path.GetExtension(path));

                File.Copy(path, tempPath, true);
                _InternalFilePath = tempPath;
            }
            else
            {
                _InternalFilePath = path;
            }

            if (Settings.Default.HoldBufferInMemory)
            {
                _InternalBuffer = File.ReadAllBytes(_InternalFilePath);
            }
        }

        public bool IsReadable(string path)
        {
            if (OdfReader.IsValidFile(path))
            {
                return true;
            }

            if (IsReadablePdf(path))
            {
                return true;
            }

            if (BinaryReader.IsValidFile(path))
            {
                return true;
            }

            if (OpenSDKReader.IsValidFile(path))
            {
                return true;
            }

            return false;
        }

        private bool IsReadablePdf(string path)
        {
            //TODO: also check headers in file.
            if (Path.GetExtension(path).ToUpper() == ".PDF")
            {
                return true;
            }
            return false;
        }

        public string ReadContents()
        {
            CheckInternalState();

            string contents = string.Empty;

            if (IsReadablePdf(_InternalFilePath))
            {
                contents = ReadPdfContents();
            }

            if (BinaryReader.IsValidFile(_InternalFilePath))
            {
                IBinaryReader reader = BinaryReader.GetNew(_InternalFilePath);
                contents = reader.ReadContents();
            }

            if (OpenSDKReader.IsValidFile(_InternalFilePath))
            {
                IOpenSDKReader reader = OpenSDKReader.GetNew(_InternalFilePath);
                contents = reader.ReadContents();
            }

            return contents;
        }

        private string ReadPdfContents()
        {
            PdfReader reader;
            if (Settings.Default.HoldBufferInMemory)
            {
                reader = new PdfReader(_InternalBuffer);
            }
            else
            {
                reader = new PdfReader(_InternalFilePath);
            }

            StringBuilder output = new StringBuilder();

            for (int i = 1; i <= reader.NumberOfPages; i++)
                output.Append(PdfTextExtractor.GetTextFromPage
                    (reader, i, new SimpleTextExtractionStrategy()));

            return output.ToString();
        }

        public string GetHash(HashType hashType)
        {
            CheckInternalState();

            string output = string.Empty;
            IHashMaker hasher = new HashMaker();

            switch (hashType)
            {
                case HashType.MD5:
                    if (Settings.Default.HoldBufferInMemory)
                    {
                        output = hasher.GetMD5(_InternalBuffer);
                    }
                    else
                    {
                        output = hasher.GetMD5(_InternalFilePath);
                    }
                    break;

                case HashType.SHA1:
                    if (Settings.Default.HoldBufferInMemory)
                    {
                        output = hasher.GetSHA1(_InternalBuffer);
                    }
                    else
                    {
                        output = hasher.GetSHA1(_InternalFilePath);
                    }
                    break;

                case HashType.SHA256:
                    if (Settings.Default.HoldBufferInMemory)
                    {
                        output = hasher.GetSHA256(_InternalBuffer);
                    }
                    else
                    {
                        output = hasher.GetSHA256(_InternalFilePath);
                    }
                    break;

                case HashType.PreHash:
                    output = hasher.GetPreHash(_InternalFilePath);
                    break;
            }

            return output;
        }

        public bool IsEqualTo(string path)
        {
            CheckInternalState();

            byte[] thisFile;

            if (Settings.Default.HoldBufferInMemory)
            {
                thisFile = _InternalBuffer;
            }
            else
            {
                thisFile = File.ReadAllBytes(_InternalFilePath);
            }

            //breakout check, if they're not even the same length
            //then definitely not the same file, should never hit this,
            //as in implementation checks are included prior to calling
            //this method.
            FileInfo file = new FileInfo(path);
            if (file.Length != thisFile.LongLength)
            {
                return false;
            }

            byte[] comparisonFile = File.ReadAllBytes(path);

            for (long i = 0; i < thisFile.LongLength-1; i++)
            {
                if (thisFile[i] != comparisonFile[i])
                {
                    return false;
                }
            }
            return true;
        }

        public void Dispose()
        {
            if (Settings.Default.MakeLocalCache)
            {
                try
                {
                    File.Delete(_InternalFilePath);
                }
                catch (Exception)
                {
                    //not a big deal if can't delete local
                    //temp file.
                }
            }
        }

        /// <summary>
        /// Checks the internal state of the instance to make
        /// sure that is has been initialised properly.
        /// </summary>
        /// <returns></returns>
        private bool IsReady()
        {
            if (_InternalFilePath == null ||
                _InternalFilePath == string.Empty)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Used to make sure that the internal state is OK, and if not
        /// then an exception is thrown.  THis should be used before trying
        /// to work with any internals of the instance.
        /// </summary>
        private void CheckInternalState()
        {
            if (IsReady() == false)
            {
                throw new InvalidOperationException(NOT_INITIALISED_MESSAGE);
            }
        }


        public bool CheckString(IEnumerable<string> toCheck)
        {
            string contents = ReadContents().ToUpper();

            foreach (string item in toCheck)
            {
                if (contents.Contains(item.ToUpper()))
                    return true;
            }
            return false;
        }

        public bool CheckRegEx(IEnumerable<string> toCheck)
        {
            string contents = ReadContents();

            foreach (string regex in toCheck)
            {
                if (Regex.IsMatch(contents, regex))
                    return true;
            }
            return false;
        }
    }
}
