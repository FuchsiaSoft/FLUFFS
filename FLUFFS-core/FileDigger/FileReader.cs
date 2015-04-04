using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDigger
{
    public class FileReader : IFileReader
    {

        public void Open(string path)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
