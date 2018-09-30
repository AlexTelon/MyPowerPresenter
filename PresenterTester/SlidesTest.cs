using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresenterCore;

namespace PresenterTester
{
    [TestClass]
    public class SlidesTest
    {
        [TestMethod]
        public void ExistanceTest()
        {
            var slide = new Slide();
        }

        [TestMethod]
        public void HasTitle()
        {
            var slide = new Slide();
            slide.Title = "test title";

            Assert.AreEqual("test title", slide.Title);
        }

        [TestMethod]
        public void SlideComparisons_NotEqual()
        {
            var slide = new Slide();
            var slide2 = new Slide();

            Assert.IsFalse(slide.Equals(slide2));
        }

        [TestMethod]
        public void SlideComparisons_Equal()
        {
            var slide = new Slide();
            var slide2 = slide;

            Assert.IsTrue(slide.Equals(slide2));
        }


        [TestMethod]
        public void HasBody()
        {
            var slide = new Slide();
            slide.Body = "body text";

            Assert.AreEqual("body text", slide.Body);
        }
    }
}
