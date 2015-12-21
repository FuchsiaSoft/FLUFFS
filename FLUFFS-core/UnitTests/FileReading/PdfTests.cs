using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileDigger;
using System.IO;
using System.Collections.Generic;

namespace UnitTests.FileReading
{
    /*
        Tests the various kinds of PDF against a set of standard strings and RegExs

        The format for the test method names and the sample files is:
        <Format>_<Version>_<Type>

        For instance Pdf_14_Simple is a Pdf document, version Pdf 1.4, and is a simple
        document with a bit of text.

        The types for documents are:
        Simple - single page document with plain text
        Complex - multiple page documents with lots of text and convoluted formatting
        Large - documents with lots of embedded media and a large file size
        TextHeavy - documents that have huge amounts of text (several hundreds of pages,
                    with the text being tested buried right in the middle)
    */



    [TestClass]
    public class PdfTests
    {

        [TestMethod]
        public void Pdf_14_Simple_CheckString()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Pdf_14_Simple.pdf");

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));
        }

        [TestMethod]
        public void Pdf_14_Complex_CheckString()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Pdf_14_Complex.pdf");

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));
        }

        [TestMethod]
        public void Pdf_14_Large_CheckString()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Pdf_14_Large.pdf");

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));
        }

        [TestMethod]
        public void Pdf_14_TextHeavy_CheckString()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Pdf_14_TextHeavy.pdf");

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));
        }

        [TestMethod]
        public void Pdf_14_Simple_CheckRegEx()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Pdf_14_Simple.pdf");

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));
        }

        [TestMethod]
        public void Pdf_14_Complex_CheckRegEx()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Pdf_14_Complex.pdf");

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));
        }

        [TestMethod]
        public void Pdf_14_Large_CheckRegEx()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Pdf_14_Large.pdf");

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));
        }

        [TestMethod]
        public void Pdf_14_TextHeavy_CheckRegEx()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Pdf_14_TextHeavy.pdf");

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));
        }

    }
}
