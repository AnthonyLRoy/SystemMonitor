using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SystemViewer.Forms;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.Toolkit.PropertyGrid;

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
            var documents =
                win.Layoutmanager.DockingManager.Layout.Descendents().OfType<LayoutAnchorable>();
                //.Count();
                if (documents.Any(x => x.ContentId != "ObjectProperties"))
                {
                    var propertiesControlContainer = win.Layoutmanager.DockingManager.Layout.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault();
                    var layoutAnchorable = new LayoutAnchorable {Title = "Properties",ContentId = "ObjectProperties"};
                    propertiesControlContainer.Children.Add(layoutAnchorable);
                    var pgrid = new PropertyGrid {AutoGenerateProperties = true, SelectedObject = layoutAnchorable};
                    pgrid.Filter = "AutoHideHeight";
                    layoutAnchorable.Content = pgrid;
                }
        }
    }
}
