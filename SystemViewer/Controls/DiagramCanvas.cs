// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagramCanvas.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The diagram canvas.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SystemViewer.Controls
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Xml;

    using SystemViewer.Designer;
    using SystemViewer.Designer.Adorners;
    using SystemViewer.Enums;
    using SystemViewer.gridClasses;
    using SystemViewer.Managers;
    using SystemViewer.Models.ToolBox;

    using BindingsHelpers;

    using Dan.monitor.Common;

    /// <summary>
    /// The diagram canvas.
    /// </summary>
    public partial class DiagramCanvas : Canvas, IDiagramCanvas
    {
        #region Fields

        /// <summary>
        /// The _diagram status.
        /// </summary>
        private DiagramStatus _diagramStatus;

        /// <summary>
        /// The controls timer.
        /// </summary>
        private ControlsTimer controlsTimer;

        /// <summary>
        /// The rubberband selection start point.
        /// </summary>
        private Point? rubberbandSelectionStartPoint;

        /// <summary>
        /// The selected items.
        /// </summary>
        private List<ISelectable> selectedItems;

        /// <summary>
        /// The selection service.
        /// </summary>
        private SelectionService selectionService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagramCanvas"/> class.
        /// </summary>
        public DiagramCanvas()
        {
            this.initialise();
            this.PrepareCommands();
            this.AllowDrop = true;
            this.WireEvents();
            EditorHelper.Register<BindingExpression, BindingConvertor>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the activate state.
        /// </summary>
        public DiagStatus ActivateState { get; set; }

        /// <summary>
        /// Gets or sets the diagram status.
        /// </summary>
        public DiagramStatus DiagramStatus
        {
            get
            {
                return this._diagramStatus;
            }

            set
            {
                this._diagramStatus = value;
                if (value == DiagramStatus.Active)
                {
                    var activeControls = (from object control in this.Children
                                          let designerItem = control as DesignerItem
                                          where designerItem != null
                                          select designerItem.Content).OfType<IMonitorControl>().ToList();
                    this.controlsTimer.Controls = activeControls;
                    this.controlsTimer.StartTimer();
                }
                else
                {
                    if (this.controlsTimer != null)
                    {
                        this.controlsTimer.StopTimer();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        public new double Height
        {
            get
            {
                return base.Height;
            }
        }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets the selected items.
        /// </summary>
        public List<ISelectable> SelectedItems
        {
            get
            {
                return this.selectedItems ?? (this.selectedItems = new List<ISelectable>());
            }

            set
            {
                this.selectedItems = value;
            }
        }

        /// <summary>
        /// Gets or sets the update frequency.
        /// </summary>
        public int UpdateFrequency { get; set; }

        /// <summary>
        /// Gets the width.
        /// </summary>
        public new double Width
        {
            get
            {
                return base.Width;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selection service.
        /// </summary>
        internal SelectionService SelectionService
        {
            get
            {
                if (this.selectionService == null)
                {
                    this.selectionService = new SelectionService(this);
                }

                return this.selectionService;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The deselect all.
        /// </summary>
        public void DeselectAll()
        {
            foreach (var item in this.SelectedItems)
            {
                item.IsSelected = false;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The measure override.
        /// </summary>
        /// <param name="constraint">
        /// The constraint.
        /// </param>
        /// <returns>
        /// The <see cref="Size"/>.
        /// </returns>
        protected override Size MeasureOverride(Size constraint)
        {
            var size = new Size();
            foreach (UIElement element in base.Children)
            {
                var left = GetLeft(element);
                var top = GetTop(element);
                left = double.IsNaN(left) ? 0 : left;
                top = double.IsNaN(top) ? 0 : top;

                // measure desired size for each child
                element.Measure(constraint);

                var desiredSize = element.DesiredSize;
                if (!double.IsNaN(desiredSize.Width) && !double.IsNaN(desiredSize.Height))
                {
                    size.Width = Math.Max(size.Width, left + desiredSize.Width);
                    size.Height = Math.Max(size.Height, top + desiredSize.Height);
                }
            }

            // add margin 
            size.Width += 10;
            size.Height += 10;
            return size;
        }

        /// <summary>
        /// The on drop.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            var dragObject = e.Data.GetData(typeof(DragObject)) as DragObject;
            if (dragObject != null && !string.IsNullOrEmpty(dragObject.Xaml))
            {
                object content = XamlReader.Load(XmlReader.Create(new StringReader(dragObject.Xaml)));

                if (content != null)
                {
                    var newItem = new DesignerItem { Content = content };

                    Point position = e.GetPosition(this);

                    if (dragObject.DesiredSize.HasValue)
                    {
                        var desiredSize = dragObject.DesiredSize.Value;
                        newItem.Width = desiredSize.Width;
                        newItem.Height = desiredSize.Height;

                        SetLeft(newItem, Math.Max(0, position.X - (newItem.Width / 2)));
                        SetTop(newItem, Math.Max(0, position.Y - (newItem.Height / 2)));
                    }
                    else
                    {
                        SetLeft(newItem, Math.Max(0, position.X));
                        SetTop(newItem, Math.Max(0, position.Y));
                    }

                    SetZIndex(newItem, this.Children.Count);
                    this.Children.Add(newItem);
                    this.SetConnectorDecoratorTemplate(newItem);

                    // update selection
                    this.SelectionService.SelectItem(newItem);
                    newItem.Focus();
                }

                e.Handled = true;
            }
        }

        /// <summary>
        /// The on mouse down.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Source.Equals(this))
            {
                this.rubberbandSelectionStartPoint = e.GetPosition(this);

                // if you click directly on the canvas all 
                // selected items are 'de-selected'
                foreach (var item in this.SelectedItems)
                {
                    item.IsSelected = false;
                }

                this.selectedItems.Clear();

                e.Handled = false;
            }

            // in case that this click is the start for a 
            // drag operation we cache the start point
        }

        /// <summary>
        /// The on mouse move.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // if mouse button is not pressed we have no drag operation, ...
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                this.rubberbandSelectionStartPoint = null;
            }

            // ... but if mouse button is pressed and start
            // point value is set we do have one
            if (this.rubberbandSelectionStartPoint.HasValue)
            {
                // create rubberband adorner
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    var adorner = new RubberbandAdorner(this, this.rubberbandSelectionStartPoint);
                    if (adorner != null)
                    {
                        adornerLayer.Add(adorner);
                    }
                }
            }

            e.Handled = true;
        }

        /// <summary>
        /// The canvas_ drag enter.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Canvas_DragEnter(object sender, DragEventArgs e)
        {
        }

        /// <summary>
        /// The canvas_ drag over.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Canvas_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }

        /// <summary>
        /// The canvas_ drop.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            var toolboxSelectedItem = (DiagramToolBoxItem)e.Data.GetData("toolboxitem");
            var activeCanvas = (Canvas)sender;
            UIElement control = ControlManager.CreateControl(toolboxSelectedItem);
            var item = new DesignerItem { Content = control, Width = 100, Height = 100 };
            activeCanvas.Children.Add(item);
            SetLeft(item, e.GetPosition(activeCanvas).X);
            SetTop(item, e.GetPosition(activeCanvas).Y);
        }

        /// <summary>
        /// The handle canvas click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void HandleCanvasClick(object sender, MouseButtonEventArgs e)
        {
            PropertiesController.Pgrid.SelectedObject = new DiagramProperties((DiagramCanvas)sender);
        }

        /// <summary>
        /// The set connector decorator template.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        private void SetConnectorDecoratorTemplate(DesignerItem item)
        {
            if (item.ApplyTemplate() && item.Content is UIElement)
            {
                var template = DesignerItem.GetConnectorDecoratorTemplate(item.Content as UIElement);
                var decorator = item.Template.FindName("PART_ConnectorDecorator", item) as Control;
                if (decorator != null && template != null)
                {
                    decorator.Template = template;
                }
            }
        }

        /// <summary>
        /// The wire events.
        /// </summary>
        private void WireEvents()
        {
            this.DiagramStatus = DiagramStatus.Design;

            // Wireup Diagram
            this.DragEnter += this.Canvas_DragEnter;
            this.MouseDown += this.HandleCanvasClick;
            this.DragOver += this.Canvas_DragOver;
            this.Drop += this.Canvas_Drop;

            // Wire up Controls
        }

        /// <summary>
        /// The initialise.
        /// </summary>
        private void initialise()
        {
            this.controlsTimer = new ControlsTimer();
            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Stretch;
            Background = SystemColors.ControlDarkDarkBrush;
            AllowDrop = true;
        }

        #endregion

        /// <summary>
        /// The drag object.
        /// </summary>
        public class DragObject
        {
            // Xaml string that represents the serialized content

            // Defines width and height of the DesignerItem
            // when this DragObject is dropped on the DesignerCanvas
            #region Public Properties

            /// <summary>
            /// Gets or sets the desired size.
            /// </summary>
            public Size? DesiredSize { get; set; }

            /// <summary>
            /// Gets or sets the xaml.
            /// </summary>
            public string Xaml { get; set; }

            #endregion
        }
    }
}