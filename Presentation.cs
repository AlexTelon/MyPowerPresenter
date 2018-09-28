using CustomPresenter.Slides;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomPresenter
{
    public class Presentation : ObservableCollection<Slide>, INotifyPropertyChanged
    {

        public Slide CurrentSlide
        {
            get
            {
                // no items -> return default empty slide
                if (Count == 0) return DefaultEmptySlide;

                // if an item has been removed one way or another make sure we dont access outside the array
                if (Index > Count - 1) Index = Count - 1;

                return this[Index];
            }
        }

        private int _index = 0;
        private int Index
        {
            get => _index;
            set
            {
                if (_index != value)
                {
                    _index = value;
                    OnPropertyChanged(nameof(CurrentSlide));
                }
            }
        }


        /// <summary>
        /// Moves presentation to next slide.
        /// Cannot go past last slide. Stays there and does nothing.
        /// </summary>
        public void Next() => Index = Math.Min(this.Count - 1, Index + 1);

        private Slide DefaultEmptySlide => new Slide();

        //public new void PropertyChanged(string propertyName) => base.PropertyChanged(propertyName);
        //public new event PropertyChangedEventHandler PropertyChanged;

        public Presentation()
        {
            this.CollectionChanged += Presentation_CollectionChanged;
        }

        private void Presentation_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // When we now have one slide and we just added it
            if (Count == 1 && e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                OnPropertyChanged(nameof(CurrentSlide));
            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                // find out if we removed our current slide
                if (e.OldStartingIndex == Index)
                {
                    OnPropertyChanged(nameof(CurrentSlide));
                }
            }
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
