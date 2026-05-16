using System;
using System.Windows.Input;

namespace SystemViewer.Infrastructure
{
    public class OpenDiagramCommand:ICommand
    {

        public event EventHandler CanExecuteChanged;
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }


        public void Execute(object parameter)
        {
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".diag";
            dlg.Filter = "Diagram files (*.diag)|*.diag";


            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
            }

        }



    }
}
