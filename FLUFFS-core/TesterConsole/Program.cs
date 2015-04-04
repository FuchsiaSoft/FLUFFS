using OdfDigger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 100; i++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                IOdfReader reader = OdfReader.GetNew("C:\\Users\\LCC\\Documents\\wedding.xlsx");
                string content = reader.ReadContents();
                stopwatch.Stop();
                Console.WriteLine(content.Length.ToString("#,###") + " characters read in: " + stopwatch.ElapsedMilliseconds + " ms");
            }

            Console.ReadLine();
        }
    }
}
