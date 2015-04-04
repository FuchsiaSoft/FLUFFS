using FileDigger.Properties;
using Hasher;
using OdfDigger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                throw new InvalidDataException(UNREADABLE_FILE_MESSAGE);
            }

            if (Settings.Default.MakeLocalCache)
            {
                string tempPath = Path.GetTempFileName();

                //lots of checks are done on file extension, so append
                //the exiting file extension.
                File.Move(tempPath, tempPath += Path.GetExtension(path));

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

            return false;
        }

        public string ReadContents()
        {
            CheckInternalState();

            string contents = string.Empty;

            if (OdfReader.IsValidFile(_InternalFilePath))
            {
                IOdfReader reader = OdfReader.GetNew(_InternalFilePath);
                contents = reader.ReadContents();
            }

            return contents;
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
    }
}
