using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;
using SystemViewer.Controls;
using SystemViewer.Forms;
using Microsoft.Win32;

namespace SystemViewer.Infrastructure
{
    public class SaveDiagramCommand:ICommand
    {
        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {

            string filename = "";
            var layout = ((MainWindow) parameter).Layoutmanager;
            var x = (DiagramCanvas) layout.DockingManager.ActiveContent;

            var saveFileDialog = new SaveFileDialog
            {
                FileName = x.Name,
                AddExtension = true,
                Filter = "Text file (*.xml)|*.xml"
            };

            if (saveFileDialog.ShowDialog() != true) return;
            filename = saveFileDialog.FileName;
            SerializeToXML(x, filename);
        }

        public static void SerializeToXML(DiagramCanvas canvas, string filename)
        {
            string mystrXaml = XamlWriter.Save(canvas);
            FileStream filestream = File.Create(filename);
            StreamWriter streamwriter = new StreamWriter(filestream);
            streamwriter.Write(mystrXaml);
            streamwriter.Close();
            filestream.Close();
        }
    }
}
