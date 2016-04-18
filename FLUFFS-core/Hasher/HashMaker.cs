using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using File = Pri.LongPath.File;
using Hasher.Properties;
using System.IO;

namespace Hasher
{
    public class HashMaker : IHashMaker
    {
        public string GetMD5(string path)
        {
            return GetMD5(File.OpenRead(path));
        }

        public string GetMD5(byte[] buffer)
        {
            return GetMD5(new MemoryStream(buffer));
        }

        public string GetMD5(Stream stream)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(stream);
            return HashToString(hash);
        }

        public string GetSHA1(string path)
        {
            return GetSHA1(File.OpenRead(path));
        }

        public string GetSHA1(byte[] buffer)
        {
            return GetSHA1(new MemoryStream(buffer));
        }

        public string GetSHA1(Stream stream)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(stream);
            return HashToString(hash);
        }

        public string GetSHA256(string path)
        {
            return GetSHA256(File.OpenRead(path));
        }

        public string GetSHA256(byte[] buffer)
        {
            return GetSHA256(new MemoryStream(buffer));
        }

        public string GetSHA256(Stream stream)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(stream);
            return HashToString(hash);
        }

        public string GetPreHash(string path)
        {
            SHA1 sha1 = SHA1.Create();
            int offset = Settings.Default.PreHashOffset;
            int size = Settings.Default.PreHashSize;
            byte[] buffer = ReadExactly(path, offset, size);
            byte[] hash = sha1.ComputeHash(buffer);
            return HashToString(hash);
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

        private byte[] ReadExactly(string path, int offset, int size)
        {
            //if the file length is less than offset+size then always just return
            //the whole file.  sometimes this will mean prehash is a bit
            //longer to run than with just 1KB designed for but still a trivial
            //time frame.  Done this way for simplicity.

            FileInfo file = new FileInfo(path);
            if (file.Length <= (offset + size))
            {
                return File.ReadAllBytes(path);
            }

            using (System.IO.Stream stream = File.OpenRead(path))
            {
                stream.Position = offset;
                byte[] buffer = new byte[size];
                int bytesRead = 0;
                do
                {
                    bytesRead += stream.Read(buffer, bytesRead, size);
                    size -= bytesRead;
                } while (size > 0);
                return buffer;
            }
        }

        
    }
}
