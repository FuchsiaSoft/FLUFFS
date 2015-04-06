using FileDigger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModel;

namespace TesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Index index = new Index();
            index.BuildIndexAsync("C:\\", "Local Drive");

            for (int i = 0; i < 100; i++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                IFileReader reader = new FileReader();
                reader.Open("C:\\users\\lcc\\documents\\lorem doc.pdf");

                stopwatch = Stopwatch.StartNew();
                string md5 = reader.GetHash(HashType.MD5);
                Console.WriteLine("MD5 in: " + stopwatch.ElapsedMilliseconds + " ms");

                stopwatch = Stopwatch.StartNew();
                string sha1 = reader.GetHash(HashType.SHA1);
                Console.WriteLine("SHA1 in: " + stopwatch.ElapsedMilliseconds + " ms");

                stopwatch = Stopwatch.StartNew();
                string sha256 = reader.GetHash(HashType.SHA256);
                Console.WriteLine("SHA256 in: " + stopwatch.ElapsedMilliseconds + " ms");

                stopwatch = Stopwatch.StartNew();
                string preHash = reader.GetHash(HashType.PreHash);
                Console.WriteLine("PreHash in: " + stopwatch.ElapsedMilliseconds + " ms");

                stopwatch = Stopwatch.StartNew();
                string content = reader.ReadContents();
                Console.WriteLine(content.Length.ToString("#,###") + " characters read in " + stopwatch.ElapsedMilliseconds + "ms");

            }

            Console.ReadLine();
        }
    }
}
