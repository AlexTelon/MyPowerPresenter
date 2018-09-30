using CustomPresenter.Properties;
using Microsoft.Win32;
using PresenterCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CustomPresenter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (!Directory.Exists(Settings.Default.CurrentFolder))
            {
                Settings.Default.CurrentFolder = System.AppDomain.CurrentDomain.BaseDirectory;
                Settings.Default.Save();
            }

            // Check if we need to ask the user which folder to load files from
            if (string.IsNullOrEmpty(Settings.Default.CurrentFile))
            {
                FileHandling.ChooseFile();
            }
        }
    }
}
