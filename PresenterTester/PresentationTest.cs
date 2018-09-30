using System;
using PresenterCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PresenterTester
{
    [TestClass]
    public class PresentationTest
    {
        [TestMethod]
        public void AddSlideExists()
        {
            var presentation = new Presentation();
            var slide = new Slide();

            presentation.Add(slide);
        }

        [TestMethod]
        public void GetCurrentSlide()
        {
            var presentation = new Presentation();
            var slide = new Slide();
            presentation.Add(slide);

            Slide current = presentation.CurrentSlide;
            Assert.AreEqual(slide, presentation.CurrentSlide);
        }

        [TestMethod]
        public void AddMultipleSlides_CheckCount()
        {
            var presentation = new Presentation();

            for (int i = 0; i < 4; i++)
            {
                presentation.Add(new Slide());
            }

            Assert.AreEqual(4, presentation.Count);
        }

        [TestMethod]
        public void CurrentSlideDefaultToFirst()
        {
            var presentation = new Presentation();

            for (int i = 0; i < 4; i++)
            {
                presentation.Add(new Slide());
            }

            Assert.AreEqual(presentation[0], presentation.CurrentSlide);
        }

        [TestMethod]
        public void CurrentSlide_AfterNext_IsSecondSlide()
        {
            var presentation = new Presentation();
            for (int i = 0; i < 4; i++)
            {
                presentation.Add(new Slide());
            }

            presentation.Next();

            Assert.AreEqual(presentation[1], presentation.CurrentSlide);
        }

        [TestMethod]
        public void CurrentSlide_AfterMultipleNext_CurrentIsCorrect()
        {
            var presentation = new Presentation();
            for (int i = 0; i < 4; i++)
            {
                presentation.Add(new Slide());
            }

            presentation.Next();
            presentation.Next();

            Assert.AreEqual(presentation[2], presentation.CurrentSlide);
        }

        [TestMethod]
        public void CurrentSlide_NextCanReachLastSlide()
        {
            var presentation = new Presentation();
            for (int i = 0; i < 4; i++)
            {
                presentation.Add(new Slide());
            }

            presentation.Next();
            presentation.Next();
            presentation.Next();

            Assert.AreEqual(presentation[3], presentation.CurrentSlide);
        }

        [TestMethod]
        public void CurrentSlide_NextCannotGoBeyondLastSlide_DoesNotThrowException()
        {
            var presentation = new Presentation();
            for (int i = 0; i < 4; i++)
            {
                presentation.Add(new Slide());
            }

            presentation.Next();
            presentation.Next();
            presentation.Next();
            presentation.Next(); // This next should do nothing.
            presentation.Next(); // This next should do nothing.

            Assert.AreEqual(presentation[3], presentation.CurrentSlide);
        }

        [TestMethod]
        public void RemoveCurrentSlide()
        {
            var presentation = new Presentation();
            for (int i = 0; i < 4; i++)
            {
                presentation.Add(new Slide());
            }

            presentation.Next();
            presentation.Next();
            presentation.Next();

            presentation.Remove(presentation.CurrentSlide);

            Assert.AreEqual(presentation[2], presentation.CurrentSlide);
        }

        [TestMethod]
        public void MultipleRemoveCurrentSlide()
        {
            var presentation = new Presentation();
            for (int i = 0; i < 3; i++)
            {
                presentation.Add(new Slide());
            }

            var lastSlide = new Slide();
            presentation.Add(lastSlide);

            presentation.Remove(presentation.CurrentSlide); // removes first
            presentation.Remove(presentation.CurrentSlide); // removes new first
            presentation.Remove(presentation.CurrentSlide); // removes new first
            // now we should only have the last one left

            Assert.AreEqual(lastSlide, presentation.CurrentSlide);
        }

        [TestMethod]
        public void RemoveAllCurrentSlide()
        {
            var presentation = new Presentation();
            for (int i = 0; i < 4; i++)
            {
                presentation.Add(new Slide());
            }

            presentation.Remove(presentation.CurrentSlide);
            presentation.Remove(presentation.CurrentSlide);
            presentation.Remove(presentation.CurrentSlide);
            presentation.Remove(presentation.CurrentSlide);

            // There should always be a default slide even when we have tried to remove all other slides
            Assert.IsNotNull(presentation.CurrentSlide);
        }

        [TestMethod]
        public void CanListenToCollectionChanged()
        {
            var presentation = new Presentation();

            var counter = 0;

            presentation.CollectionChanged += (sender, arg) =>
            {
                counter++;
            };

            // add 4 times
            for (int i = 0; i < 4; i++)
            {
                presentation.Add(new Slide());
            }

            // remove 4 times
            presentation.Remove(presentation.CurrentSlide);
            presentation.Remove(presentation.CurrentSlide);
            presentation.Remove(presentation.CurrentSlide);
            presentation.Remove(presentation.CurrentSlide);

            Assert.AreEqual(8, counter);
        }

        [TestMethod]
        public void CurrentSlideChangesCorrectly_Add()
        {
            var presentation = new Presentation();

            var counter = 0;

            presentation.PropertyChanged += (sender, arg) =>
            {
                counter++;
            };

            // adding should only change CurrentSlide 1 time
            for (int i = 0; i < 4; i++)
            {
                presentation.Add(new Slide());
            }

            Assert.AreEqual(1, counter);
        }

        [TestMethod]
        public void CurrentSlideChangesCorrectly_AddThenRemove()
        {
            var presentation = new Presentation();

            var counter = 0;

            presentation.PropertyChanged += (sender, arg) =>
            {
                counter++;
            };

            // adding should only change CurrentSlide 1 time
            for (int i = 0; i < 4; i++)
            {
                presentation.Add(new Slide());
            }

            // removing currentSlide should update each time
            presentation.Remove(presentation.CurrentSlide);
            presentation.Remove(presentation.CurrentSlide);
            presentation.Remove(presentation.CurrentSlide);
            presentation.Remove(presentation.CurrentSlide);


            Assert.AreEqual(5, counter);
        }

        [TestMethod]
        public void CurrentSlideChangesCorrectly_Next()
        {
            var presentation = new Presentation();

            var counter = 0;

            presentation.PropertyChanged += (sender, arg) =>
            {
                counter++;
            };

            // adding should only change CurrentSlide 1 time
            for (int i = 0; i < 4; i++)
            {
                presentation.Add(new Slide());
            }

            // 3 next, all should update
            presentation.Next();
            presentation.Next();
            presentation.Next();

            Assert.AreEqual(4, counter);
        }


        [TestMethod]
        public void CurrentSlideChangesCorrectly_NextAtEndNoNotify()
        {
            var presentation = new Presentation();

            var counter = 0;

            presentation.PropertyChanged += (sender, arg) =>
            {
                counter++;
            };

            // adding should only change CurrentSlide 1 time
            for (int i = 0; i < 4; i++)
            {
                presentation.Add(new Slide());
            }
            // current is index 0
            presentation.Next(); // current - index 1 (notify!)
            presentation.Next(); // current - index 2 (notify!)
            presentation.Next(); // current - index 3 (notify!)

            // all these extra should not send a new notification
            presentation.Next();
            presentation.Next();
            presentation.Next();
            presentation.Next();
            presentation.Next();
            presentation.Next();

            Assert.AreEqual(4, counter);
        }



        [TestClass]
        public class PresentationPreviousTests
        {
            [TestMethod]
            public void CurrentSlideChangesCorrectly_NextPrevious()
            {
                var presentation = new Presentation();

                // adding should only change CurrentSlide 1 time
                for (int i = 0; i < 4; i++)
                {
                    presentation.Add(new Slide());
                }

                presentation.Next();
                presentation.Previous();

                Assert.AreEqual(presentation[0], presentation.CurrentSlide);
            }

            [TestMethod]
            public void PreviousCannotMovePastStart()
            {
                var presentation = new Presentation();

                var slide = new Slide();
                presentation.Add(slide);

                presentation.Previous();

                Assert.AreEqual(slide, presentation.CurrentSlide);
            }

            [TestMethod]
            public void PreviousCausesCurrentSlidePropertyChanges()
            {
                var presentation = new Presentation();

                var counter = 0;

                presentation.PropertyChanged += (sender, arg) =>
                {
                    counter++;
                };

                // adding should only change CurrentSlide 1 time
                for (int i = 0; i < 4; i++)
                {
                    presentation.Add(new Slide());
                }

                // 3 next
                presentation.Next();
                presentation.Next();
                presentation.Next();

                // 3 previous
                presentation.Previous();
                presentation.Previous();
                presentation.Previous();

                // no more!
                presentation.Previous();
                presentation.Previous();
                presentation.Previous();

                Assert.AreEqual(7, counter);
            }



        }

    }
}
