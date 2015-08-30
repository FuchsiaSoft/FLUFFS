using Pri.LongPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSDKDigger
{
    /// <summary>
    /// An implementation of IOpenSDKReader that handles all
    /// common office files using Open SDK.  It will internally
    /// deal with the logic around whether it is a Word/Excel
    /// file etc.
    /// </summary>
    public abstract class OpenSDKReader : IOpenSDKReader
    {
        /// <summary>
        /// The message that will be included in an exception
        /// message should the file format not be supported.
        /// </summary>
        private const string NOT_VALID_FILE_MESSAGE =
            "The file specified is not a supported file that can be parsed";

        /// <summary>
        /// The path to the file to be read.
        /// </summary>
        protected string _FilePath;

        /// <summary>
        /// The list of Word file formats that are known to work with
        /// this library.
        /// </summary>
        private static List<string> _WordOpenSDKExtensions = new List<string>()
        {
            ".DOCX"
        };

        /// <summary>
        /// The list of PowerPoint file formats that are known to work with
        /// this library.
        /// </summary>
        private static List<string> _PowerPointOpenSDKExtensions = new List<string>()
        {
            ".PPTX"
        };

        /// <summary>
        /// Returns a new IOpenSDKReader that can read the contents
        /// of the provided format document.  If the file format
        /// is not supported, then an InvalidDataException will
        /// be thrown.  It is possible to do a pre-check using
        /// the IsValidFile method.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IOpenSDKReader GetNew(string path)
        {
            string extension = Path.GetExtension(path).ToUpper();

            IOpenSDKReader reader = null;

            if (_WordOpenSDKExtensions.Contains(extension))         
            {
                reader = new WordReader(path);
                return reader;
            }

            if (_PowerPointOpenSDKExtensions.Contains(extension))
            {
                reader = new PowerPointReader(path);
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
            if (_WordOpenSDKExtensions.Contains(extension))
                return true;
            if (_PowerPointOpenSDKExtensions.Contains(extension))
                return true;
            return false;
        }
    }
}
