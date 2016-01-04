using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Pri.LongPath.File;

namespace BinaryDigger
{
    /// <summary>
    /// Provides an implementation of IBinaryReader
    /// specifically for pre-2003 Excel files.
    /// </summary>

    //internal class ExcelReader : BinaryReader
    public class ExcelReader : BinaryReader
    {
        public ExcelReader(string path)
        {
            _FilePath = path;
        }

        /// <summary>
        /// Reads the contents of a pre-2003 Excel file and returns the contents as a single string.
        /// </summary>
        /// <returns>Contents of any 2003 and prior Excel files (.xls) as a single string</returns>
        public override string ReadContents()
        {
            Stream stream = new MemoryStream(File.ReadAllBytes(_FilePath));

            DataTableCollection worksheets;
            StringBuilder sb = new StringBuilder();

            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream))
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
