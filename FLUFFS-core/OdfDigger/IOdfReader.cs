using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdfDigger
{
    /// <summary>
    /// Provides the interface for reading ODF office
    /// file contents into a single string of all their
    /// text.
    /// </summary>
    public interface IOdfReader
    {
        /// <summary>
        /// Reads the contents of an ODF file into a
        /// single text string.
        /// </summary>
        /// <param name="path">The path of the file
        /// to read the contents from.</param>
        /// <returns>The contents of the ODF file</returns>
        string ReadContents(string path);
    }
}
