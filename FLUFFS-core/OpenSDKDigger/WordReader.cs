using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSDKDigger
{
    /// <summary>
    /// Provides an implementation of IOpenSDKReader specifically
    /// for Word format files.
    /// </summary>
    internal class WordReader : OpenSDKReader
    {
        public WordReader(string path)
        {
            _FilePath = path;
        }

        public override string ReadContents()
        {
            string documentContent = null;
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(_FilePath, false))
            {
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    documentContent = sr.ReadToEnd();

                    // Now search any Comments in the document as well.
                    WordprocessingCommentsPart wordProcessingCommentsPart = wordDoc.MainDocumentPart.WordprocessingCommentsPart;
                    if (wordProcessingCommentsPart != null)
                    {
                        using (StreamReader sr1 = new StreamReader(wordProcessingCommentsPart.GetStream()))
                        {
                            documentContent += sr1.ReadToEnd();
                        }
                    }
                }
            }
            return documentContent;
        }
    }
}
