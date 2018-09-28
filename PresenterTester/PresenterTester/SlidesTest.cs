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
            var title = slide.Title;
        }

    }
}
