using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDigger
{
    /// <summary>
    /// Provides the facility for reading the contents of common
    /// office files, as well as generating hashes for them
    /// and checking if the subject file is equal to another
    /// specified file using a parallel byte comparison.
    /// </summary>
    public interface IFileReader : IDisposable
    {
        /// <summary>
        /// Prepares the instance for working on the specified
        /// file by generating a local cache of it.  This method
        /// must be called before attempting any file access
        /// or processing operations.  If the file is not valid
        /// then an InvalidDataException is thrown.  To avoid this
        /// call the IsReadable method.
        /// </summary>
        /// <param name="path">The path to the file that should
        /// be opened.</param>
        void Open(string path);

        /// <summary>
        /// Checks if the specified file is supported for processing.
        /// </summary>
        /// <param name="path">The path to the file that should
        /// be checked.</param>
        /// <returns>True if the file is supported.</returns>
        bool IsReadable(string path);

        /// <summary>
        /// Reads the contents of the file into a single string.
        /// </summary>
        /// <returns>A string of the file contents.</returns>
        string ReadContents();

        /// <summary>
        /// Checks the contents of the file to see if any of
        /// the specified strings occur in its contents.
        /// </summary>
        /// <param name="toCheck">The list of strings to check.</param>
        /// <returns>True if any of the strings occur.</returns>
        bool CheckString(IEnumerable<string> toCheck);

        /// <summary>
        /// Checks the contents of the file to see if any of
        /// the specified regular expressions have matches.
        /// </summary>
        /// <param name="toCheck">The list of regular expressions
        /// to check.</param>
        /// <returns>True of any of the regular expressions
        /// have matches.</returns>
        bool CheckRegEx(IEnumerable<string> toCheck);

        /// <summary>
        /// Performs a hash of the specified type on the
        /// opened file.
        /// </summary>
        /// <param name="hashType">The type of hash to perform</param>
        /// <returns>A string of the hash value</returns>
        string GetHash(HashType hashType);

        /// <summary>
        /// Compares whether the opened file is equal to (byte for byte)
        /// the specified file.
        /// </summary>
        /// <param name="path">The path to the file that should
        /// be checked for equality.</param>
        /// <returns>True if the specified file is identical
        /// to the opened file.</returns>
        bool IsEqualTo(string path);
    }
}
