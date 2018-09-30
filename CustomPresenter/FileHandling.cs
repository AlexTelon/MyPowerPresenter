using CustomPresenter.Properties;
using Microsoft.Win32;
using PresenterCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPresenter
{
    public static class FileHandling
    {
        public static void ChooseFile()
        {
            // Displays an OpenFileDialog so the user can select a Cursor.  
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = $"Presentation Files|*." + string.Join("*.", LoadPresentation.ValidFileEndings);
            openFileDialog.Title = "Select a Presntation File";
            openFileDialog.InitialDirectory = Settings.Default.CurrentFolder;

            if (openFileDialog.ShowDialog() == true)
            {
                Settings.Default.CurrentFile = openFileDialog.FileName;
                Settings.Default.CurrentFolder = Path.GetDirectoryName(openFileDialog.FileName);
                Settings.Default.Save();
            }
            else
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
    }
}
