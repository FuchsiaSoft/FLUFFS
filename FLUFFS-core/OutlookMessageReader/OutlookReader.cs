using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Path = Pri.LongPath.Path;
using File = Pri.LongPath.File;
using System.IO;
using System.Text.RegularExpressions;

namespace OutlookMessageReader
{
    public class OutlookReader : IOutlookReader
    {
        public OutlookReader(string path)
        {
            if (IsValidFile(path) == false)
            {
                throw new InvalidDataException(NOT_VALID_FILE_MESSAGE);
            }
            _FilePath = path;
        }

        private static string _HtmlRegExPattern = 
            @"(<script(\s|\S)*?<\/script>)|(<style(\s|\S)*?<\/style>)|(<!--(\s|\S)*?-->)|(<\/?(\s|\S)*?>)";

        /// <summary>
        /// The message that will be included in an exception
        /// message should the file format not be supported.
        /// </summary>
        private const string NOT_VALID_FILE_MESSAGE =
            "The file specified is not a supported " +
            "Outlook file that can be parsed";

        /// <summary>
        /// The list of Outlook extensions that are known to work with this library
        /// </summary>
        private static List<string> _OutlookExtensions = new List<string>()
        {
            ".MSG"
        };

        /// <summary>
        /// The path to the file to be read.
        /// </summary>
        protected string _FilePath;

        /// <summary>
        /// Returns a new instance of IOutlookReader for the provided
        /// file path.
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Will throw if the path
        /// does not match a known Outlook extensions</exception>
        public static IOutlookReader GetNew(string path)
        {
            return new OutlookReader(path);
        }

        public string ReadContents()
        {
            MsgReader.Reader reader = new MsgReader.Reader();

            string tempMsgFilePath = GetTempLocalFile();

            DirectoryInfo tempDirectory = new DirectoryInfo(Path.GetTempPath());
            DirectoryInfo tempOutputDirectory = tempDirectory.CreateSubdirectory(Guid.NewGuid().ToString());

            reader.ExtractToFolder(tempMsgFilePath, tempOutputDirectory.FullName);

            StringBuilder builder = new StringBuilder();

            Regex regex = new Regex(_HtmlRegExPattern);

            foreach (FileInfo file in tempOutputDirectory.EnumerateFiles())
            {
                if (file.Extension.ToUpper() == ".HTM")
                {
                    string fileContent = File.ReadAllText(file.FullName);
                    fileContent = regex.Replace(fileContent, string.Empty);
                    builder.Append(fileContent);
                }
            }

            try
            {
                File.Delete(tempMsgFilePath);
                tempOutputDirectory.Delete(true);
            }
            catch (Exception) { }

            return builder.ToString();
        }


        /// <summary>
        /// This method creates a temporary local copy of the file to read,
        /// because the MsgReader library doesn't support long paths, or
        /// streams that can be retrieved from the longpath library.
        /// Therefore longpath makes a copy of the file for us, and we
        /// work with the locally copied file.
        /// </summary>
        /// <returns></returns>
        private string GetTempLocalFile()
        {
            string tempPath = Path.GetTempPath() +
                    Guid.NewGuid().ToString() + ".msg";

            File.Copy(_FilePath, tempPath, true);

            return tempPath;
        }


        public static bool IsValidFile(string path)
        {
            string extension = Path.GetExtension(path).ToUpper();
            if (_OutlookExtensions.Contains(extension))
            {
                return true;
            }
            return false;
        }

        public IEnumerable<string> GetEmbeddedFiles()
        {
            MsgReader.Reader reader = new MsgReader.Reader();

            string tempMsgFilePath = GetTempLocalFile();

            DirectoryInfo tempDirectory = new DirectoryInfo(Path.GetTempPath());
            DirectoryInfo tempOutputDirectory = tempDirectory.CreateSubdirectory(Guid.NewGuid().ToString());

            reader.ExtractToFolder(tempMsgFilePath, tempOutputDirectory.FullName);

            List<string> tempFiles = new List<string>();

            foreach (FileInfo file in tempOutputDirectory.EnumerateFiles())
            {
                //HTM files will be the email itself, which is covered by reading contents
                //DB files are to exclude thumbs.db files
                if (file.Extension.ToUpper() != ".HTM" &&
                    file.Extension.ToUpper() != ".DB")
                {
                    string thisTempFile = tempDirectory + 
                        Guid.NewGuid().ToString() + 
                        Path.GetExtension(file.Name);

                    File.Copy(file.FullName, thisTempFile);

                    tempFiles.Add(thisTempFile);
                }
            }

            try
            {
                File.Delete(tempMsgFilePath);
                tempOutputDirectory.Delete(true);
            }
            catch (Exception) { }

            return tempFiles;
        }
    }
}
