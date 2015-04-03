using System;
using System.IO;
using System.IO.Compression;

namespace OdfDigger
{
    /// <summary>
    /// Provides a facility to unpack ODF office files.
    /// The class implementes IDisposable and will delete
    /// the temp folder that it unpacks upon disposal.
    /// </summary>
    internal class OdfUnpacker : IDisposable
    {
        private string _TempFolder;

        /// <summary>
        /// Unpacks an ODF file and returns a string representing
        /// the folder of its contents.  A reference is retained internally
        /// for disposal of the temp folder when the instance
        /// is disposed.
        /// </summary>
        /// <param name="path">The path of the file to unpack</param>
        /// <returns>The path of the unpacked folder</returns>
        public string UnpackOdf(string path)
        {
            _TempFolder = Path.GetTempPath() + Path.GetRandomFileName();
            Directory.CreateDirectory(_TempFolder);

            ZipFile.ExtractToDirectory(path, _TempFolder);
            return _TempFolder;
        }

        public void Dispose()
        {
            if (Directory.Exists(_TempFolder))
            {
                try
                {
                    Directory.Delete(_TempFolder, true);
                }
                catch (Exception)
                {
                    //just cleaning local temp files, no biggie
                    //if it can't clean up the odd one for this
                    //usage case.
                }
            }
        }
    }
}
