using System;
using System.Windows.Input;
using SystemViewer.Forms;

namespace SystemViewer.Infrastructure
{
    public class LoadPropertiesWindowCommand:ICommand
    {
        private const string ObjectPropertiesContainer = "ObjectProperties";
        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var win = (MainWindow)parameter;
            
            win.Layoutmanager.AddWindowToRight(PropertiesController.Pgrid ,"Properties", ObjectPropertiesContainer);
           
        }
    }
}
