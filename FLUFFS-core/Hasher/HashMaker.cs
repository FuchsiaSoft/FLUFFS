using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Pri.LongPath;

namespace Hasher
{
    class HashMaker : IHashMaker
    {
        public string GetMD5(string path)
        {
            return GetMD5(File.ReadAllBytes(path));
        }

        public string GetMD5(byte[] buffer)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(buffer);
            return HashToString(hash);
        }

        public string GetSHA1(string path)
        {
            return GetSHA1(File.ReadAllBytes(path));
        }

        public string GetSHA1(byte[] buffer)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(buffer);
            return HashToString(hash);
        }

        public string GetSHA256(byte[] buffer)
        {
            throw new NotImplementedException();
        }

        public string GetPreHash(byte[] buffer)
        {
            throw new NotImplementedException();
        }

        private string HashToString(byte[] hash)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte chunk in hash)
            {
                builder.Append(chunk.ToString("X2"));
            }
            return builder.ToString();
        }
    }
}
