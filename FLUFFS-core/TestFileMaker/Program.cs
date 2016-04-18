using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using MigraDoc;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;

namespace TestFileMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 1000; i++)
            {
                MigraDoc.DocumentObjectModel.Document doc = new Document();
                Section section = doc.AddSection();

                string randomText = FerretClientUI.Utils.RandomWordGenerator.GenerateRandomText
                    (2000, FerretClientUI.Utils.CaseOption.LowerCase, FerretClientUI.Utils.NumberOption.NoNumbers);

                section.AddParagraph(randomText);

                MigraDoc.Rendering.PdfDocumentRenderer renderer = new PdfDocumentRenderer();
                renderer.Document = doc;
                renderer.RenderDocument();
                renderer.Save($"W:\\ICTSERV\\01APPDEVSUP\\ContractorTests\\{i.ToString("0000")}.pdf");
            }

        }
    }
}
