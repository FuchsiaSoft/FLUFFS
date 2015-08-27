using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSDKDigger
{
    /// <summary>
    /// Provides the interface for reading office
    /// file contents into a single string of all their
    /// text using Open SDK
    /// </summary>
    public interface IOpenSDKReader
    {
        /// <summary>
        /// Reads the contents of an Office file into a single string
        /// </summary>
        /// <returns>The contents of the file</returns>
        string ReadContents();
    }
}
