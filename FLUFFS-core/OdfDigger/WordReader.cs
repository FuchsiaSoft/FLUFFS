using System.IO;
using System.Xml.Linq;

namespace OdfDigger
{
    /// <summary>
    /// Provides an implementation of IOdfReader specifically
    /// for Word format files.
    /// </summary>
    internal class WordReader : IOdfReader
    {
        public string ReadContents(string path)
        {
            using (OdfUnpacker packer = new OdfUnpacker())
            {
                string newFolder = packer.UnpackOdf(path);
                string xmlFileLocation = newFolder + "\\word\\document.xml";
                string xmlContent = File.ReadAllText(xmlFileLocation);

                XDocument xDoc = XDocument.Parse(xmlContent);
                string documentContent = xDoc.Root.Value;
                return documentContent;
            }
        }
    }
}
