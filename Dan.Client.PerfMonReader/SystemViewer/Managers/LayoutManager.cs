// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LayoutManager.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The layout manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using SystemViewer.Controls;
    using SystemViewer.gridClasses;
    using SystemViewer.Models;

    using Dan.Common.Messages;
    using Dan.monitor.Common;

    using Xceed.Wpf.AvalonDock;
    using Xceed.Wpf.AvalonDock.Layout;

    /// <summary>
    /// The layout manager.
    /// </summary>
    public class LayoutManager
    {
        #region Fields

        /// <summary>
        /// The _active documents.
        /// </summary>
        private readonly Dictionary<string, Canvas> _activeDocuments = new Dictionary<string, Canvas>();

        /// <summary>
        /// The _docking manager.
        /// </summary>
        private DockingManager _dockingmanager;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutManager"/> class.
        /// </summary>
        /// <param name="dockingmanager">
        /// The docking manager.
        /// </param>
        public LayoutManager(DockingManager dockingmanager)
        {
            // TODO: Complete member initialization
            this._dockingmanager = dockingmanager;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the docking manager.
        /// </summary>
        public DockingManager DockingManager
        {
            get
            {
                return this._dockingmanager;
            }

            set
            {
                this._dockingmanager = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add diagram.
        /// </summary>
        /// <param name="getdiagram">
        /// The get diagram.
        /// </param>
        /// <param name="diagramName">
        /// The diagram name.
        /// </param>
        public void AddDiagram(Func<string, DiagramCanvas> getdiagram, string diagramName)
        {
            // Does the name already exist
            if (this._activeDocuments.ContainsKey(diagramName))
            {
                return;
            }

            var diagram = getdiagram(diagramName);
            this._activeDocuments.Add(diagramName, diagram);
            var firstdocument = this._dockingmanager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
            if (firstdocument != null)
            {
                var doc2 = new LayoutDocument { Title = diagramName, Content = diagram };
                doc2.IsSelected = true;
                var sv = new ScrollViewer
                             {
                                 HorizontalScrollBarVisibility = ScrollBarVisibility.Auto, 
                                 VerticalScrollBarVisibility = ScrollBarVisibility.Auto, 
                                 VerticalAlignment = VerticalAlignment.Stretch, 
                                 HorizontalAlignment = HorizontalAlignment.Stretch, 
                                 Content = diagram
                             };
                doc2.Content = sv;
                firstdocument.Children.Add(doc2);
            }

            foreach (var control in diagram.Children.OfType<IMonitorControl>())
            {
                ((UIElement)control).MouseDown += this.control_MouseDown;
            }
        }

        void doc2_IsActiveChanged(object sender, EventArgs e)
        {
        }


        public DiagramCanvas GetActiveDiagram()
        {

            try
            {
                var y = this._dockingmanager.Layout.Descendents().OfType<LayoutDocument>().Where(x => x.IsActive);
                var z = (DiagramCanvas)((ScrollViewer)y.First().Content).Content;
                return z;
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Select the diagram that you want to save by clicking on it",
                    "Select the document",
                    MessageBoxButton.OK,
                    MessageBoxImage.Asterisk);
            }

            return null;

        }

        /// <summary>
        /// The add new diagram.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public void AddNewDiagram(string name)
        {
            this.AddDiagram(this.CreateNewCanvas, name);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The add window to right.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="displayname">
        /// The display name.
        /// </param>
        /// <param name="objectPropertiesContainername">
        /// The object properties container name.
        /// </param>
        internal void AddWindowToRight(object item, string displayname, string objectPropertiesContainername)
        {
            var layoutAnchorablePaneGroup =
                this._dockingmanager.Layout.Descendents().OfType<LayoutAnchorablePaneGroup>().FirstOrDefault();
            if (layoutAnchorablePaneGroup != null)
            {
                var anchorepane = layoutAnchorablePaneGroup.Children;
                if (
                    anchorepane.Any(
                        layoutAnchorablePane =>
                        layoutAnchorablePane.Descendents()
                            .OfType<LayoutAnchorable>()
                            .Any(x => x.ContentId == displayname)))
                {
                    return;
                }

                var documentContainer = new LayoutAnchorable
                                            {
                                                Title = displayname, 
                                                ContentId = displayname, 
                                                Content = item, 
                                            };
                ((LayoutAnchorablePane)anchorepane.First()).InsertChildAt(0, documentContainer);
            }
        }

        /// <summary>
        /// The add windows to left.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="contentId">
        /// The content id.
        /// </param>
        internal void AddWindowsToLeft(object item, string name, string contentId)
        {
            // Find out if it contains the anchor
            LayoutAnchorGroup panelContainer =
                this._dockingmanager.Layout.Descendents().OfType<LayoutAnchorGroup>().FirstOrDefault();
            var documentContainer = new LayoutAnchorable { Title = name, ContentId = contentId };
            if (panelContainer != null)
            {
                panelContainer.Children.Add(documentContainer);
            }

            documentContainer.Content = item;
        }

        /// <summary>
        /// The get active diagrams.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        internal List<DiagramRef> GetActiveDiagrams()
        {
            return this._activeDocuments.Keys.ToList().Select(x => new DiagramRef(x, x, x, x)).ToList();
        }

        /// <summary>
        /// The create new canvas.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="DiagramCanvas"/>.
        /// </returns>
        private DiagramCanvas CreateNewCanvas(string name)
        {
            var diagrambackground = new DiagramCanvas
                                        {
                                            Name = name.Replace(" ", string.Empty), 
                                            VerticalAlignment = VerticalAlignment.Stretch, 
                                            HorizontalAlignment = HorizontalAlignment.Stretch, 
                                            Background = SystemColors.ControlDarkDarkBrush, 
                                            AllowDrop = true
                                        };

            return diagrambackground;
        }

        /// <summary>
        /// The control_ mouse down.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void control_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            var control = (IMonitorControl)sender;
            PropertiesController.Pgrid.AutoGenerateProperties = true;
            PropertiesController.Pgrid.SelectedObject = new DiagramControlProperties(control);
        }

        #endregion
    }
}