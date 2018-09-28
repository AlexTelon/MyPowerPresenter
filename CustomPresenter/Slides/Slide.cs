using CustomPresenter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPresenter.Slides
{
    public class Slide : BindableBase
    {
        public string Title
        {
            get => Get<string>();
            set => Set(value);
        }

        public Slide()
        {
            Title = "Default slide title";
        }

    }
}
