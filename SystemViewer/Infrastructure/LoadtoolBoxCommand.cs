using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SystemViewer.Forms;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace SystemViewer.Infrastructure
{
    public class LoadToolBoxCommand : ICommand
    {
        private const string ObjectPropertiesContainer = "ObjectProperties";

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var win = (MainWindow) parameter;
            win.Layoutmanager.AddWindowsToLeft(new Controls.Windowtoolbox(),"tool box","toolboxContainer");
            

        }
    }
}
