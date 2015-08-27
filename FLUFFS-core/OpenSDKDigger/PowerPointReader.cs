using A = DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSDKDigger
{
    internal class PowerPointReader : OpenSDKReader
    {
        public PowerPointReader(string path)
        {
            _FilePath = path;
        }

        public override string ReadContents()
        {
            int numberOfSlides = CountSlides(_FilePath);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < numberOfSlides; i++)
            {
                sb.Append(GetSlideIdAndText(_FilePath, i));
            }

            return sb.ToString();
        }

        private int CountSlides(string presentationFile)
        {
            using (PresentationDocument presentationDocument = PresentationDocument.Open(presentationFile, false))
            {
                // Pass the presentation to the next CountSlides method and return the results
                return CountSlides(presentationDocument);
            }
        }

        private int CountSlides(PresentationDocument presentationDocument)
        {
            if (presentationDocument == null)
            {
                throw new ArgumentNullException("presentationDocument");
            }

            int slidesCount = 0;

            // Get the presentation part of document and get the slide count from the SlideParts.
            PresentationPart presentationPart = presentationDocument.PresentationPart;
            if (presentationPart != null)
            {
                slidesCount = presentationPart.SlideParts.Count();
            }

            return slidesCount;
        }

        public static string GetSlideIdAndText(string docName, int index)
        {
            StringBuilder paragraphText = new StringBuilder();

            using (PresentationDocument ppt = PresentationDocument.Open(docName, false))
            {
                // Get the relationship ID of the first slide.
                PresentationPart part = ppt.PresentationPart;
                OpenXmlElementList slideIds = part.Presentation.SlideIdList.ChildElements;

                string relId = (slideIds[index] as SlideId).RelationshipId;

                // Get the slide part from the relationship ID.
                SlidePart slide = (SlidePart)part.GetPartById(relId);

                // Get the inner text of the slide
                IEnumerable<A.Text> texts = slide.Slide.Descendants<A.Text>();
                foreach (A.Text text in texts)
                {
                    paragraphText.Append(text.Text);
                }

                // Get any notes from the slide
                if (slide.NotesSlidePart != null)
                {
                    paragraphText.Append(slide.NotesSlidePart.NotesSlide.InnerText);
                }
            }
            return paragraphText.ToString();
        }
    }
}