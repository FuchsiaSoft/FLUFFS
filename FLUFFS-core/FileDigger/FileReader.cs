using FileDigger.Properties;
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

        private string _OriginalPath;

        private byte[] _InternalBuffer = null;

        private const string UNREADABLE_FILE_MESSAGE =
            "The specified file is not supported.";

        public void Open(string path)
        {
            if (IsReadable(path) == false)
            {
                throw new InvalidDataException(UNREADABLE_FILE_MESSAGE);
            }

            if (Settings.Default.MakeLocalCache)
            {
                string tempPath = Path.GetTempFileName();
                File.Copy(path, tempPath);
                _InternalFilePath = tempPath;
            }
            else
            {
                _OriginalPath = path;
            }

            if (Settings.Default.HoldBufferInMemory)
            {
                if (Settings.Default.MakeLocalCache)
                {
                    _InternalBuffer = File.ReadAllBytes(_InternalFilePath);
                }
                else
                {
                    _InternalBuffer = File.ReadAllBytes(_OriginalPath);
                }
            }
        }

        public bool IsReadable(string path)
        {
            throw new NotImplementedException();
        }

        public string ReadContents()
        {
            throw new NotImplementedException();
        }

        public string GetHash(HashType hashType)
        {
            throw new NotImplementedException();
        }

        public bool IsEqualTo(string path)
        {
            byte[] thisFile;

            if (Settings.Default.HoldBufferInMemory)
            {
                thisFile = _InternalBuffer;
            }
            else
            {
                if (Settings.Default.MakeLocalCache)
                {
                    thisFile = File.ReadAllBytes(_InternalFilePath);
                }
                else
                {
                    thisFile = File.ReadAllBytes(_OriginalPath);
                }
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
    }
}
