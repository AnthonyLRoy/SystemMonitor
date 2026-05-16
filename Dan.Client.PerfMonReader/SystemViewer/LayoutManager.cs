using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SystemViewer.Models;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;

namespace SystemViewer
{
    public class LayoutManager
    {
        private Xceed.Wpf.AvalonDock.DockingManager _dockingmanager;

        private readonly Dictionary<string, Canvas> _activeDocuments = new Dictionary<string, Canvas>();
        public LayoutManager(Xceed.Wpf.AvalonDock.DockingManager dockingmanager)
        {
            // TODO: Complete member initialization
            _dockingmanager = dockingmanager;
        }

        public void LoadDiagram(string name)
        {
        }

        public void AddNewDiagram(string name)
        {
            var diagrambackground = new Canvas
            {
                Name = name,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = SystemColors.ControlDarkDarkBrush
            };
            _activeDocuments.Add(name, diagrambackground);

            var firstdocument = _dockingmanager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
            if (firstdocument != null)
            {
                var doc2 = new LayoutDocument {Title = name, Content = diagrambackground};
                firstdocument.Children.Add(doc2);
            }

        }

        public DockingManager DockingManager
        {
            get { return _dockingmanager; }
            set { _dockingmanager = value; }
        }

        internal List<DiagramRef> GetActiveDiagrams()
        {
            return _activeDocuments.Keys.ToList().Select(x => new DiagramRef(x, x, x, x)).ToList();
        }
    }
}
