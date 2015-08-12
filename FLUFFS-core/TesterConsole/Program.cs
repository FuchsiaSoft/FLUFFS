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
            // GetHashes();

            List<string> searchTerms = new List<string>() { "election" };
            
            // Search ODF Word file (.DOCX or .DOCM)
            SearchDocumentsForStrings(@"C:\Ferret\SampleFiles\ODFWordFile.docx", searchTerms);

            // Search Pre-2003 Excel file (.XLS)
            SearchDocumentsForStrings(@"C:\Ferret\SampleFiles\Pre2003ExcelFile.xls", searchTerms);

            // Search ODF Excel file (.XLSX or .XLSM)
            SearchDocumentsForStrings(@"C:\Ferret\SampleFiles\ODFExcelFile.xlsx", searchTerms);

            // Search ODF PowerPoint files (.PPTX)
            SearchDocumentsForStrings(@"C:\Ferret\SampleFiles\ODFPowerPointFile.pptx", searchTerms);

            Console.WriteLine("DONE!");
            Console.ReadLine();
        }

        private static void SearchDocumentsForStrings(string documentName, List<string> searchTerms)
        {
            Console.WriteLine("Now searching " + documentName);
            IFileReader reader = new FileReader();
            reader.Open(documentName);
            if (reader.CheckString(searchTerms))
            {
                Console.WriteLine("Search term found");
            }
            else
            {
                Console.WriteLine("Search term not found");
            }
        }

        private static void GetHashes()
        {
            Index index = new Index();
            index.BuildIndexAsync("C:\\Users", "Local Profiles");

            for (int i = 0; i < 100; i++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                IFileReader reader = new FileReader();
                reader.Open("C:\\Users\\LCC\\Documents\\lorem doc.docx");

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
        }
    }
}
