using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hasher
{
    interface IHashMaker
    {
        string GetMD5(string path);
        string GetMD5(byte[] buffer);

        string GetSHA1(string path);
        string GetSHA1(byte[] buffer);

        string GetSHA256(byte[] buffer);

        string GetPreHash(byte[] buffer);
    }
}
