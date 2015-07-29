using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryDigger
{
    /// <summary>
    /// Provides the interface for reading pre-2007 office
    /// file contents into a single string of all their
    /// text.
    /// </summary>
    public interface IBinaryReader
    {
        /// <summary>
        /// Reads the contents of a pre-2007 Office file into a single string
        /// </summary>
        /// <returns>The contents of the file</returns>
        string ReadContents();
    }
}
