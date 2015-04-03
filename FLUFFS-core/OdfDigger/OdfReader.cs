using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdfDigger
{
    /// <summary>
    /// An implementation of IOdfReader that handles all
    /// common office based ODF files.  It will internally
    /// deal with the logic around whether it is a Word/Excel
    /// file etc.
    /// </summary>
    public class OdfReader : IOdfReader
    {
        /// <summary>
        /// The message that will be included in an exception
        /// message should the file format not be supported.
        /// </summary>
        private const string NOT_VALID_FILE_MESSAGE =
            "The file specified is not a supported " +
            "ODF file that can be parsed";

        /// <summary>
        /// The list of Word file formats that are known to work with
        /// this library.
        /// </summary>
        private static List<string> _WordOdfExtensions = new List<string>()
        {
            ".DOCX", ".DOCM"
        };

        /// <summary>
        /// The list of Excel file formats that are known to work with
        /// this library.
        /// </summary>
        private static List<string> _ExcelOdfExtensions = new List<string>()
        {
            ".XLSX", ".XLSM"
        };

        /// <summary>
        /// Pulls the contents of a supported ODF office file 
        /// into a single string.
        /// </summary>
        /// <param name="path">The file to extract the contents from</param>
        /// <returns>A string of the contents of the file.</returns>
        public string ReadContents(string path)
        {
            IOdfReader reader = null;
            string extension = Path.GetExtension(path).ToUpper();

            if (_WordOdfExtensions.Contains(extension))
            {
                reader = new WordReader();
            }

            if (_ExcelOdfExtensions.Contains(extension))
            {
                reader = new ExcelReader();
            }

            if (reader == null)
            {
                throw new InvalidDataException(NOT_VALID_FILE_MESSAGE);
            }

            return reader.ReadContents(path);
        }
    }
}
