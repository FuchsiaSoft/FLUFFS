using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileDigger;

namespace UnitTests.FileReading
{

    /*
        Tests the various kinds of Word document against a set of standard strings and RegExs

        The format for the test method names and the sample files is:
        <Format>_<Version>_<Type>

        For instance Word_03_Simple is a Word document, version 97 - 03 (the old binary .doc format)
        and is a simple document with a bit of text.

        The types for documents are:
        Simple - single page document with plain text
        Complex - multiple page documents with lots of text and convoluted formatting
        Large - documents with lots of embedded media and a large file size
        TextHeavy - documents that have huge amounts of text (several hundreds of pages,
                    with the text being tested buried right in the middle)
    */

    [TestClass]
    public class WordTests
    {
        [TestMethod]
        public void Word_03_Simple_CheckString()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Word_03_Simple.doc");

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));
        }

        [TestMethod]
        public void Word_03_Complex_CheckString()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Word_03_Complex.doc");

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));
        }

        [TestMethod]
        public void Word_03_Large_CheckString()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Word_03_Large.doc");

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));
        }

        [TestMethod]
        public void Word_03_TextHeavy_CheckString()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Word_03_TextHeavy.doc");

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));
        }

        [TestMethod]
        public void Word_03_Simple_CheckRegEx()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Word_03_Simple.doc");

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));
        }

        [TestMethod]
        public void Word_03_Complex_CheckRegEx()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Word_03_Complex.doc");

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));
        }

        [TestMethod]
        public void Word_03_Large_CheckRegEx()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Word_03_Large.doc");

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));
        }

        [TestMethod]
        public void Word_03_TextHeavy_CheckRegEx()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Word_03_TextHeavy.doc");

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));
        }

        [TestMethod]
        public void Word_03Template_Simple_CheckString()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Word_03Template_Simple.dot");

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));
        }

        [TestMethod]
        public void Word_03Template_Complex_CheckString()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Word_03Template_Complex.dot");

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));
        }

        [TestMethod]
        public void Word_03Template_Large_CheckString()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Word_03Template_Large.dot");

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));
        }

        [TestMethod]
        public void Word_03Template_TextHeavy_CheckString()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Word_03Template_TextHeavy.dot");

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));
        }

        [TestMethod]
        public void Word_03Template_Simple_CheckRegEx()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Word_03Template_Simple.dot");

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));
        }

        [TestMethod]
        public void Word_03Template_Complex_CheckRegEx()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Word_03Template_Complex.dot");

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));
        }

        [TestMethod]
        public void Word_03Template_Large_CheckRegEx()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Word_03Template_Large.dot");

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));
        }

        [TestMethod]
        public void Word_03Template_TextHeavy_CheckRegEx()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Word_03Template_TextHeavy.dot");

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));
        }

    }
}
