using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileDigger;

namespace UnitTests.FileReading
{

    /*
        Tests the various kinds of Word document against a set of standard strings and RegExs

        The format for the test method names and the sample files is:
        <Format>_<Version>_<Type>

        For instance Excel_03_Simple is an Excel book, version 97 - 03 (the old binary .doc format)
        and is a simple document with a bit of text.

        The types for documents are:
        Simple - single page document with plain text
        Complex - multiple page documents with lots of text and convoluted formatting
        Large - documents with lots of embedded media and a large file size
        TextHeavy - documents that have huge amounts of text (several hundreds of pages,
                    with the text being tested buried right in the middle)
    */

    [TestClass]
    public class ExcelTests
    {
        #region Simple Files

        [TestMethod]
        public void Excel_03_Simple_CheckString()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Excel_03_Simple.xls");

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));
        }

        [TestMethod]
        public void Excel_03_Simple_CheckRegEx()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Excel_03_Simple.xls");

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));
        }

        [TestMethod]
        public void Excel_03Template_Simple_CheckString()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Excel_03Template_Simple.xlt");

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));
        }

        [TestMethod]
        public void Excel_03Template_Simple_CheckRegEx()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Excel_03Template_Simple.xlt");

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));
        }

        [TestMethod]
        public void Excel_07_Simple_CheckString()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Excel_07_Simple.xlsx");

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));
        }

        [TestMethod]
        public void Excel_07_Simple_CheckRegEx()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Excel_07_Simple.xlsx");

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));
        }


        [TestMethod]
        public void Excel_07Template_Simple_CheckString()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Excel_07Template_Simple.xltx");

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));
        }

        [TestMethod]
        public void Excel_07Template_Simple_CheckRegEx()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Excel_07Template_Simple.xltx");

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));
        }

        [TestMethod]
        public void Excel_95_Simple_CheckString()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Excel_95_Simple.xls");

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));
        }

        [TestMethod]
        public void Excel_95_Simple_CheckRegEx()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Excel_95_Simple.xls");

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));
        }

        #endregion
    }
}
