using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresenterCore;

namespace PresenterTester
{
    [TestClass]
    public class LoadPresentationTest
    {
        [TestMethod]
        public void Existance()
        {
            Presentation presentation = LoadPresentation.CreatePresentationFromFileContents(new string[] { "" });
            Assert.IsNotNull(presentation);
        }

        [TestMethod]
        public void Title()
        {
            Presentation presentation = LoadPresentation.CreatePresentationFromFileContents(new string[] { "#Title" });
            Assert.AreEqual("Title", presentation[0].Title);
        }

        [TestMethod]
        public void MultipleTitlesOnly()
        {
            var contents = new string[] { "#Title1", "#Title2", "#Title3", "#some other name" };

            Presentation presentation = LoadPresentation.CreatePresentationFromFileContents(contents);

            Assert.AreEqual("Title1", presentation[0].Title);
            Assert.AreEqual("Title2", presentation[1].Title);
            Assert.AreEqual("Title3", presentation[2].Title);
            Assert.AreEqual("some other name", presentation[3].Title);
        }

        [TestMethod]
        public void EmptyTitle()
        {
            // should we allow an empty title? I guess so, it might be convinient to just have a body?
            var contents = new string[] { "#" };

            Presentation presentation = LoadPresentation.CreatePresentationFromFileContents(contents);

            Assert.AreEqual("", presentation[0].Title);
        }

        [TestMethod]
        public void NoTitle_ButThereIsBody()
        {
            // should we allow an empty title? I guess so, it might be convinient to just have a body?
            var contents = new string[] { "Body" };
            Assert.ThrowsException<InvalidDataException>(() => 
                LoadPresentation.CreatePresentationFromFileContents(contents));
        }

        [TestMethod]
        public void TitleAfterBody()
        {
            // should we allow an empty title? I guess so, it might be convinient to just have a body?
            var contents = new string[] { "Body", "#Title" };
            Assert.ThrowsException<InvalidDataException>(() =>
                LoadPresentation.CreatePresentationFromFileContents(contents));
        }

        [TestMethod]
        public void TitleAndBody()
        {
            var contents = new string[] { "#Title", "Body" };

            Presentation presentation = LoadPresentation.CreatePresentationFromFileContents(contents);
            Assert.AreEqual("Title", presentation[0].Title);
            Assert.AreEqual("Body", presentation[0].Body);
        }

        [TestMethod]
        public void TitleAndTwoLinesBody()
        {
            var contents = new string[] { "#Title", "Body line 1", "Body line 2" };

            Presentation presentation = LoadPresentation.CreatePresentationFromFileContents(contents);
            Assert.AreEqual("Title", presentation[0].Title);
            Assert.AreEqual(contents[1] + "\n" + contents[2], presentation[0].Body);
        }

        [TestMethod]
        public void TitleAndMultipleLinesBody()
        {
            var contents = new string[] { "#Title", "Body line 1", "Body line 2", "Body line 3" };

            Presentation presentation = LoadPresentation.CreatePresentationFromFileContents(contents);
            Assert.AreEqual("Title", presentation[0].Title);
            Assert.AreEqual(contents[1] + "\n" + contents[2] + "\n" + contents[3], presentation[0].Body);
        }


        [TestMethod]
        public void TwoSlidesWithTitleAndBody()
        {
            var contents = new string[] { "#Title1", "Body line 1", "Body line 2", "#Title2", "Body line 1", "Body line 2", };

            Presentation presentation = LoadPresentation.CreatePresentationFromFileContents(contents);

            Assert.AreEqual("Title1", presentation[0].Title);
            Assert.AreEqual(contents[1] + "\n" + contents[2], presentation[0].Body);

            Assert.AreEqual("Title2", presentation[1].Title);
            Assert.AreEqual(contents[4] + "\n" + contents[5], presentation[1].Body);
        }



        [TestMethod]
        public void MultipleSlidesWithTitleAndBody()
        {
            var contents = new string[] { "#Title1", "Body line 1", "Body line 2",
                                          "#Title2", "Body line 1", "Body line 2",
                                          "#Title3", "",};

            Presentation presentation = LoadPresentation.CreatePresentationFromFileContents(contents);

            Assert.AreEqual("Title1", presentation[0].Title);
            Assert.AreEqual(contents[1] + "\n" + contents[2], presentation[0].Body);

            Assert.AreEqual("Title2", presentation[1].Title);
            Assert.AreEqual(contents[4] + "\n" + contents[5], presentation[1].Body);

            Assert.AreEqual("Title3", presentation[2].Title);
            Assert.AreEqual("", presentation[2].Body);
        }


        [TestMethod]
        public void LoadFileWithEmptyString()
        {
            Assert.ThrowsException<ArgumentException>(() => LoadPresentation.LoadFromFile(""));
        }

        [TestMethod]
        public void LoadNonExistentFile()
        {
            Assert.ThrowsException<FileNotFoundException>(() =>
                LoadPresentation.LoadFromFile("this file should not exist i really hope"));
        }

    }
}
