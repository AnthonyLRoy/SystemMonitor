using System;
using System.Windows.Input;
using SystemViewer.Forms;
using SystemViewer.Forms.dialogs;

namespace SystemViewer.Infrastructure
{
    public class NewDiagramCommand:ICommand
    {

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var win = (MainWindow) parameter;
            frmNewDiagram diagram = new frmNewDiagram();
            diagram.ShowDialog();
            if (diagram.DialogResult == true)
            {
                win.Layoutmanager.AddNewDiagram(diagram.DiagramName);
            }

           
        }
    }
}
