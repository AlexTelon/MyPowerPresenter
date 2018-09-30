using Microsoft.Win32;
using PresenterCore;
using System.IO;

namespace CustomPresenter
{
    public static class FileHandling
    {
        /// <summary>
        /// Only lets the user choose which file should be the default one. Does not load the file in anywhere or something.
        /// </summary>
        public static void ChooseCurrentFile(PresenterSettings settings)
        {
            // Displays an OpenFileDialog so the user can select a Cursor.  
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = $"Presentation Files|*." + string.Join("*.", LoadPresentation.ValidFileEndings);
            openFileDialog.Title = "Select a Presntation File";
            openFileDialog.InitialDirectory = settings.CurrentFolder;

            if (openFileDialog.ShowDialog() == true)
            {
                settings.CurrentFile = openFileDialog.FileName;
                settings.CurrentFolder = Path.GetDirectoryName(openFileDialog.FileName);
                settings.Save();
            }
        }
    }
}
