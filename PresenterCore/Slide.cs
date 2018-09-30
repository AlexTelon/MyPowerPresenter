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
            Title = "Default slide title";
            Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            Footer = "Alex Telon";
        }

    }
}
