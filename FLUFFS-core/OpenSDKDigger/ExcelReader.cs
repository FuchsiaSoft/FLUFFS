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

            DataTableCollection worksheets;
            StringBuilder sb = new StringBuilder();

            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
            {
                worksheets = excelReader.AsDataSet().Tables;
                excelReader.Close();
            }

            foreach (DataTable sheet in worksheets)
            {
                foreach (DataRow row in sheet.Rows)
                {
                    foreach (var cell in row.ItemArray)
                    {
                        sb.Append(cell.ToString());
                    }
                }
            }

            return sb.ToString();
        }


    }
}
