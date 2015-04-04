using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hasher
{
    /// <summary>
    /// Provides a convenient way of making hashes
    /// using the built in hashers of .Net; When using
    /// PreHash it is usually better to specify the path
    /// than the buffer performance wise, unless the buffer
    /// is maintained in memory already.
    /// </summary>
    public interface IHashMaker
    {
        /// <summary>
        /// Generates MD5 hash for the specified
        /// file.
        /// </summary>
        /// <param name="path">The file to hash.</param>
        /// <returns>string representing the hash.</returns>
        string GetMD5(string path);

        /// <summary>
        /// Generates MD5 hash for the specified
        /// file.
        /// </summary>
        /// <param name="buffer">The file to hash.</param>
        /// <returns>string representing the hash.</returns>
        string GetMD5(byte[] buffer);

        /// <summary>
        /// Generates SHA1 hash for the specified
        /// file.
        /// </summary>
        /// <param name="path">The file to hash.</param>
        /// <returns>string representing the hash.</returns>
        string GetSHA1(string path);

        /// <summary>
        /// Generates SHA1 hash for the specified
        /// file.
        /// </summary>
        /// <param name="buffer">The file to hash.</param>
        /// <returns>string representing the hash.</returns>
        string GetSHA1(byte[] buffer);

        /// <summary>
        /// Generates SHA256 hash for the specified
        /// file.
        /// </summary>
        /// <param name="path">The file to hash.</param>
        /// <returns>string representing the hash.</returns>
        string GetSHA256(string path);

        /// <summary>
        /// Generates SHA256 hash for the specified
        /// file.
        /// </summary>
        /// <param name="buffer">The file to hash.</param>
        /// <returns>string representing the hash.</returns>
        string GetSHA256(byte[] buffer);

        //NOTE: prehash does not allow for a buffer, string
        //only due to its behaviour.

        /// <summary>
        /// Generates PreHash hash for the specified
        /// file.
        /// </summary>
        /// <param name="path">The file to hash.</param>
        /// <returns>string representing the hash.</returns>
        string GetPreHash(string path);
    }
}
