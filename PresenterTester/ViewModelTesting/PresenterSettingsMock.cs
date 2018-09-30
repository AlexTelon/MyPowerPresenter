using CustomPresenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresenterTester.ViewModelTesting
{
    class PresenterSettingsMock : PresenterSettings
    {
        public override System.Drawing.Color BackgroundColor { get; set; }

        public override System.Drawing.Color Foreground { get; set; }

        public override string CurrentFolder { get; set; }

        public override string CurrentFile { get; set; }

        public override void Save() {}
    }
}
