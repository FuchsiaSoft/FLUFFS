using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdfDigger
{
    /// <summary>
    /// Provides an implementation of IOdfReader
    /// specifically for Excel format files.
    /// </summary>
    internal class ExcelReader : IOdfReader
    {
        public string ReadContents(string path)
        {

            /*
             * Excel files are slightly more complex than Word
             * files for parsing.  For efficiency they store
             * strings in a shared XML file regardless of
             * worksheet location.  This is easy to retrieve and is
             * treated the same as for Word.  But formulas and non
             * text based values are stored in individual XML files
             * for each worksheet, in the xl\worksheets folder.
             */ 

            //TODO: in line with comment above, make this support
            //more than the shared string extraction.

            using (OdfUnpacker packer = new OdfUnpacker())
            {
                string newFolder = packer.UnpackOdf(path);
                string sharedXmlLocation = newFolder + "\\sharedStrings.xml";

            }
        }
    }
}
