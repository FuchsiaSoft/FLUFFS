using Pri.LongPath;
using System.Collections.Generic;

namespace OdfDigger
{
    /// <summary>
    /// An implementation of IOdfReader that handles all
    /// common office based ODF files.  It will internally
    /// deal with the logic around whether it is a Word/Excel
    /// file etc.
    /// </summary>
    public abstract class OdfReader : IOdfReader
    {
        /// <summary>
        /// The message that will be included in an exception
        /// message should the file format not be supported.
        /// </summary>
        private const string NOT_VALID_FILE_MESSAGE =
            "The file specified is not a supported " +
            "ODF file that can be parsed";

        /// <summary>
        /// The path to the file to be read.
        /// </summary>
        protected string _FilePath;

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

        private static List<string> _PowerPointOdfExtensions = new List<string>()
        {
            ".PPTX"
        };

        /// <summary>
        /// Returns a new IOdfReader that can read the contents
        /// of the provided Odf format document.  If the file format
        /// is not supported, then an InvalidDataException will
        /// be thrown.  It is possible to do a pre-check using
        /// the IsValidFile method.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IOdfReader GetNew(string path)
        {
            string extension = Path.GetExtension(path).ToUpper();

            IOdfReader reader = null;

            if (_WordOdfExtensions.Contains(extension))
            {
                reader = new WordReader(path);
                return reader;
            }

            if (_ExcelOdfExtensions.Contains(extension))
            {
                reader = new ExcelReader(path);
                return reader;
            }

            if (_PowerPointOdfExtensions.Contains(extension))
            {
                reader = new PowerPointReader(path);
                return reader;
            }
            throw new System.IO.InvalidDataException(NOT_VALID_FILE_MESSAGE);
        }

        /// <summary>
        /// Pulls the contents of a supported ODF office file 
        /// into a single string.
        /// </summary>
        /// <returns>A string of the contents of the file.</returns>
        public abstract string ReadContents();


        /// <summary>
        /// Checks whether the file is a supported format
        /// </summary>
        /// <param name="path">The path of the file
        /// to check format</param>
        /// <returns>true if the format is supported</returns>
        public static bool IsValidFile(string path)
        {
            string extension = Path.GetExtension(path).ToUpper();
            if (_WordOdfExtensions.Contains(extension))
                return true;
            if (_ExcelOdfExtensions.Contains(extension))
                return true;
            if (_PowerPointOdfExtensions.Contains(extension))
                return true;
            return false;
        }
    }
}
