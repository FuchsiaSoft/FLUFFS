using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookMessageReader
{
    /// <summary>
    /// Provides an interface for reading Outlook .MSG file
    /// contents
    /// </summary>
    public interface IOutlookReader
    {
        /// <summary>
        /// Reads the contents of an Outlook file into a single string.
        /// </summary>
        /// <returns></returns>
        string ReadContents();


        /// <summary>
        /// Returns an <see cref="IEnumerable{string}"/>}"/> of temp paths
        /// to any embedded files.  IMPORTANT: the files are extracted from the email
        /// and temp files are created to have their paths returned by this method.
        /// The temp files will not be deleted by the reader and the caller (i.e. you!)
        /// is responsible for deleting the temp files explicitly when you are
        /// done with them.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetEmbeddedFiles();
    }
}
