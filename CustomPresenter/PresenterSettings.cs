using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPresenter
{
    public class PresenterSettings
    {
        private Properties.Settings _settings;

        public PresenterSettings()
        {
            _settings = Properties.Settings.Default;
        }

        public static PresenterSettings Default => _lazy.Value;
        // Singelton pattern where we only create a PresenterSetting when needed and only once.
        private static readonly Lazy<PresenterSettings> _lazy = 
            new Lazy<PresenterSettings>(() => new PresenterSettings());


        public virtual System.Drawing.Color BackgroundColor
        {
            get => _settings.BackgroundColor;
            set => _settings.BackgroundColor = value;
        }

        public virtual System.Drawing.Color Foreground
        {
            get => _settings.Foreground;
            set => _settings.Foreground = value;
        }

        public virtual string CurrentFolder
        {
            get => _settings.CurrentFolder;
            set => _settings.CurrentFolder = value;
        }

        public virtual string CurrentFile
        {
            get => _settings.CurrentFile;
            set => _settings.CurrentFile = value;
        }

        public virtual void Save() => _settings.Save();
    }
}
