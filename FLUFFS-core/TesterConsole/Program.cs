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

            for (int i = 0; i < 10; i++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                IFileReader reader = new FileReader("C:\\Temp\\Purchase Ordering Procedures.xlsx");
                string output = reader.ReadContents();

                Console.WriteLine("Speed Test:  " + stopwatch.Elapsed.TotalMinutes);
                Console.WriteLine(output.Substring(0, 30));
            }

            Console.WriteLine("Done, press any key to exit");

            Console.ReadKey();
        }
        
    }
}
