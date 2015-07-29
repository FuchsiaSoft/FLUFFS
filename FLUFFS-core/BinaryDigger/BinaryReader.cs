using Pri.LongPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryDigger
{
    /// <summary>
    /// An implementation of IBinaryReader that handles all
    /// common pre-2003 office files.  It will internally
    /// deal with the logic around whether it is a Word/Excel
    /// file etc.
    /// </summary>
    public abstract class BinaryReader : IBinaryReader
    {
        /// <summary>
        /// The message that will be included in an exception
        /// message should the file format not be supported.
        /// </summary>
        private const string NOT_VALID_FILE_MESSAGE =
            "The file specified is not a supported " +
            "pre-2003 file that can be parsed";

        /// <summary>
        /// The path to the file to be read.
        /// </summary>
        protected string _FilePath;

        /// <summary>
        /// The list of Word file formats that are known to work with
        /// this library.
        /// </summary>
        private static List<string> _WordBinaryExtensions = new List<string>()
        {
            ".DOC"
        };

        /// <summary>
        /// The list of Excel file formats that are known to work with
        /// this library.
        /// </summary>
        private static List<string> _ExcelBinaryExtensions = new List<string>()
        {
            ".XLS"
        };

        /// <summary>
        /// Returns a new IBinaryReader that can read the contents
        /// of the provided binary format document.  If the file format
        /// is not supported, then an InvalidDataException will
        /// be thrown.  It is possible to do a pre-check using
        /// the IsValidFile method.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IBinaryReader GetNew(string path)
        {
            string extension = Path.GetExtension(path).ToUpper();

            IBinaryReader reader = null;

            //if (_WordBinaryExtensions.Contains(extension))
            //{
            //    reader = new WordReader(path);
            //    return reader;
            //}

            if (_ExcelBinaryExtensions.Contains(extension))
            {
                reader = new ExcelReader(path);
                return reader;
            }
            throw new System.IO.InvalidDataException(NOT_VALID_FILE_MESSAGE);
        }

        /// <summary>
        /// Pulls the contents of a supported pre-2003 office file 
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
            if (_WordBinaryExtensions.Contains(extension))
                return true;
            if (_ExcelBinaryExtensions.Contains(extension))
                return true;
            return false;
        }
    }
}
