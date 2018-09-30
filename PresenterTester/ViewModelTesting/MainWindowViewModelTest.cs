using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomPresenter;
using PresenterTester.ViewModelTesting;
using System.IO;

namespace PresenterTester
{
    [TestClass]
    public class MainWindowViewModelTest
    {
        [TestMethod]
        public void CanLoadPresentation()
        {
            // Setup.
            var mockSettings = new PresenterSettingsMock();
            var currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
            var file = Path.Combine(currentDir, $"{nameof(MainWindowViewModelTest)}.md");

            mockSettings.CurrentFile = file;

            File.WriteAllText(mockSettings.CurrentFile, "#Title \nBody");

            // Execute.
            var vm = new MainWindowViewModel(mockSettings);

            // Check.
            Assert.AreEqual("Title", vm.Presentation.CurrentSlide.Title);
            Assert.AreEqual("Body", vm.Presentation.CurrentSlide.Body);

            // Teardown.
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        [TestMethod]
        public void NextSlide()
        {
            // Setup.
            var mockSettings = new PresenterSettingsMock();
            var currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
            var file = Path.Combine(currentDir, $"{nameof(MainWindowViewModelTest)}.md");

            mockSettings.CurrentFile = file;

            File.WriteAllText(mockSettings.CurrentFile, "#Title1 \nBody\n#Title2\nBody2");

            // Execute.
            var vm = new MainWindowViewModel(mockSettings);

            vm.NextSlideCommand.Execute(null);

            // Check.
            Assert.AreEqual("Title2", vm.Presentation.CurrentSlide.Title);
            Assert.AreEqual("Body2", vm.Presentation.CurrentSlide.Body);

            // Teardown.
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        [TestMethod]
        public void PreviousSlide_WhenThereIsNone()
        {
            // Setup.
            var mockSettings = new PresenterSettingsMock();
            var currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
            var file = Path.Combine(currentDir, $"{nameof(MainWindowViewModelTest)}.md");

            mockSettings.CurrentFile = file;

            File.WriteAllText(mockSettings.CurrentFile, "#Title1 \nBody1\n#Title2\nBody2");

            // Execute.
            var vm = new MainWindowViewModel(mockSettings);

            vm.PreviousSlideCommand.Execute(null);

            // Check.
            Assert.AreEqual("Title1", vm.Presentation.CurrentSlide.Title);
            Assert.AreEqual("Body1", vm.Presentation.CurrentSlide.Body);

            // Teardown.
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }


        [TestMethod]
        public void NextPreviousSlide()
        {
            // Setup.
            var mockSettings = new PresenterSettingsMock();
            var currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
            var file = Path.Combine(currentDir, $"{nameof(MainWindowViewModelTest)}.md");

            mockSettings.CurrentFile = file;

            File.WriteAllText(mockSettings.CurrentFile, "#Title1 \nBody1\n#Title2\nBody2");

            // Execute.
            var vm = new MainWindowViewModel(mockSettings);

            vm.NextSlideCommand.Execute(null);
            vm.PreviousSlideCommand.Execute(null);

            // Check.
            Assert.AreEqual("Title1", vm.Presentation.CurrentSlide.Title);
            Assert.AreEqual("Body1", vm.Presentation.CurrentSlide.Body);

            // Teardown.
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

    }
}
