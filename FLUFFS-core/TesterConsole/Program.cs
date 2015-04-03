using OdfDigger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IOdfReader reader = new OdfReader();
            Console.WriteLine(reader.ReadContents("G:\\FileDiggerTest\\SpreadsheetTest.xlsx"));
            Console.ReadLine();
        }
    }
}
