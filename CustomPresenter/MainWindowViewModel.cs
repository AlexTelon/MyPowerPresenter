using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomPresenter.Slides;
using CustomPresenter.Utils;

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

            Presentation = new Presentation();

            for (int i = 0; i < 4; i++)
            {
                var slide = new Slide();
                slide.Title = i.ToString();
                Presentation.Add(slide);
            }


        }


        public RelayCommand<object> NextSlideCommand { get; internal set; }

        private void SetUpCommands()
        {
            NextSlideCommand = new RelayCommand<object>(OnNextSlide);
        }

        private void OnNextSlide(object obj)
        {
            Presentation.Next();
        }
    }
}
