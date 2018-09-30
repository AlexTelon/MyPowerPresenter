using PresenterCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresenterCore
{
    public class Slide : BindableBase
    {
        public string Title
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Body
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Footer
        {
            get => Get<string>();
            set => Set(value);
        }

        public Slide()
        {
        }

    }
}
