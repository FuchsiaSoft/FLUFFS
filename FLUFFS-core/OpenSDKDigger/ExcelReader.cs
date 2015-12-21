using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Pri.LongPath.File;

namespace OpenSDKDigger
{
    internal class ExcelReader : OpenSDKReader 
    {
        public ExcelReader(string path)
        {
            _FilePath = path;
        }

        /// <summary>
        /// Reads the contents of a 2007+ Excel file and returns the contents as a single string.
        /// </summary>
        /// <returns>Contents of any 2007+ Excel files (.xlsx etc) as a single string</returns>
        public override string ReadContents()
        {
            Stream stream = new MemoryStream(File.ReadAllBytes(_FilePath));
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            // get all the worksheets in the spreadsheet
            List<string> sheets = getWorkSheetNames(excelReader).ToList<string>();

            StringBuilder sb = new StringBuilder();
            foreach (var sheet in sheets)
            {
                var workSheet = excelReader.AsDataSet().Tables[sheet];
                var rows = from DataRow a in workSheet.Rows select a;
                foreach (var row in rows)
                {
                    for (int i = 0; i <= row.ItemArray.GetUpperBound(0); i++)
                    {
                        sb.Append(row[i].ToString());
                    }
                }
            }

            excelReader.Close();

            return sb.ToString();
        }

        /// <summary>
        /// An excel file can contain multiple worksheets.  This gets the name of 
        /// each worksheet in the file
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>The names of each worksheet</returns>
        public IEnumerable<string> getWorkSheetNames(IExcelDataReader reader)
        {
            var sheets = from DataTable sheet in reader.AsDataSet().Tables select sheet.TableName;
            return sheets;
        }
    }
}
