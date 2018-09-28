using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomPresenter.Utils;

namespace CustomPresenter
{
    class MainWindowViewModel : BindableBase
    {
        public string HelloWorld
        {
            get => Get<string>();
            set => Set(value);
        }

        public ObservableCollection<string> CollectionOfWords { get; set; } = new ObservableCollection<string>() { "a", "few", "words" };

        public MainWindowViewModel()
        {
            HelloWorld = "hello world test string";
        }
    }
}
