using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileDigger;

namespace UnitTests.FileReading
{
    [TestClass]
    public class OutlookTests
    {
        /* 
            The tests for Outlook generally follow a pattern, this is...

            Check for strings and regexes, for emails both with and without attachments.
            For those without attachments, the text to match is in the body of the email,
            and for those with attachments the text to match is in the body of the attachment.

            There will be multiple assertions to make sure that the email successfully reads,
            for instance when checking the email without attachments, it should successfully read
            the body, and it should also fail on an email where the text is in an attahcment.

    */

        [TestMethod]
        public void Msg_ReadWithoutAttemptingAttachments_CheckString()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Msg_NoAttachments.msg");

            reader.IncludeEmbeddedFiles = false;

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));

            reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Msg_PdfAttachment.msg");

            reader.IncludeEmbeddedFiles = false;

            Assert.IsFalse(reader.CheckString(TestConstants.StringsToCheck));
        }

        [TestMethod]
        public void Msg_ReadWithoutAttemptingAttachments_CheckRegEx()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Msg_NoAttachments.msg");

            reader.IncludeEmbeddedFiles = false;

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));

            reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Msg_PdfAttachment.msg");

            reader.IncludeEmbeddedFiles = false;

            Assert.IsFalse(reader.CheckRegEx(TestConstants.RegExToCheck));
        }

        [TestMethod]
        public void Msg_ReadAndAttemptAttachments_CheckString()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Msg_NoAttachments.msg");

            reader.IncludeEmbeddedFiles = true;

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));

            reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Msg_PdfAttachment.msg");

            reader.IncludeEmbeddedFiles = true;

            Assert.IsTrue(reader.CheckString(TestConstants.StringsToCheck));
        }

        [TestMethod]
        public void Msg_ReadAndAttemptAttachments_CheckRegEx()
        {
            IFileReader reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Msg_NoAttachments.msg");

            reader.IncludeEmbeddedFiles = true;

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));

            reader = new FileReader
                (Environment.CurrentDirectory + "\\FileReading\\SampleFiles\\Msg_PdfAttachment.msg");

            reader.IncludeEmbeddedFiles = true;

            Assert.IsTrue(reader.CheckRegEx(TestConstants.RegExToCheck));
        }

    }
}
