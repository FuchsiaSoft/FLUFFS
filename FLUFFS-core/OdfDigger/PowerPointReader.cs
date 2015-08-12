using Pri.LongPath;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Linq;

namespace OdfDigger
{
    /// <summary>
    /// Provides an implementation of IOdfReader specifically
    /// for PowerPoint (PPTX) format files.
    /// </summary>
    internal class PowerPointReader : OdfReader
    {
        public PowerPointReader(string path)
        {
            _FilePath = path;
        }

        public override string ReadContents()
        {
            /*
             * The content from all PowerPoint slides are stored in their own XML file (one file for
             * each slide).  Likewise, the contents of each note for a slide is stored in it's own 
             * XML file.
             * Theses files can be found in the directories \\ppt\slides and \\ppt\\notesslides
             */

            using (OdfUnpacker packer = new OdfUnpacker())
            {
                string newFolder = packer.UnpackOdf(_FilePath);

                List<string> fileArray = new List<string>();
                if (Pri.LongPath.Directory.Exists(newFolder + "\\ppt\\slides"))
                {
                    fileArray.AddRange(Pri.LongPath.Directory.GetFiles(newFolder + "\\ppt\\slides", "*.xml", SearchOption.AllDirectories).ToList());
                }

                if (Pri.LongPath.Directory.Exists(newFolder + "\\ppt\\notesslides"))
                {
                    fileArray.AddRange(Pri.LongPath.Directory.GetFiles(newFolder + "\\ppt\\notesslides", "*.xml", SearchOption.AllDirectories).ToList());
                }

                string xmlContent = string.Empty;
                string documentContent = string.Empty;
                foreach (var file in fileArray)
                {
                    xmlContent = Pri.LongPath.File.ReadAllText(file);
                    XDocument xDoc = XDocument.Parse(xmlContent);
                    documentContent += xDoc.Root.Value;
                }
                return documentContent;
            }
        }
    }
}