
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
        /// <returns>The contents of the ODF file</returns>
        string ReadContents();
    }
}
