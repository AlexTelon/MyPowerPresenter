using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomPresenter.Properties;
using CustomPresenter.Utils;
using PresenterCore;
using PresenterCore.Utils;

namespace CustomPresenter
{
    class MainWindowViewModel : BindableBase
    {

        public Presentation Presentation
        {
            get => Get<Presentation>();
            set => Set(value);
        }

        public MainWindowViewModel()
        {
            SetUpCommands();

            if (!File.Exists(Settings.Default.CurrentFile))
            {
                FileHandling.ChooseCurrentFile();
            }

            LoadCurrentFile();
        }

        public void LoadCurrentFile()
        {
            Presentation = LoadPresentation.LoadFromFile(Settings.Default.CurrentFile);
        }

        public RelayCommand<object> NextSlideCommand { get; internal set; }
        public RelayCommand<object> PreviousSlideCommand { get; internal set; }
        public RelayCommand<object> LoadPresentationCommand { get; internal set; }

        private void SetUpCommands()
        {
            NextSlideCommand = new RelayCommand<object>(OnNextSlide);
            PreviousSlideCommand = new RelayCommand<object>(OnPreviousSlide);
            LoadPresentationCommand = new RelayCommand<object>(OnLoadPresentation);
        }

        private void OnNextSlide(object obj) => Presentation.Next();

        private void OnPreviousSlide(object obj) => Presentation.Previous();

        private void OnLoadPresentation(object obj)
        {
            FileHandling.ChooseCurrentFile();
            LoadCurrentFile();
        }
    }
}
