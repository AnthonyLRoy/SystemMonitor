// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Connector.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The connector.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Designer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;

    using SystemViewer.Controls;
    using SystemViewer.Designer.Adorners;

    /// <summary>
    /// The connector.
    /// </summary>
    public class Connector : Control, INotifyPropertyChanged
    {
        // drag start point, relative to the DesignerCanvas
        #region Fields

        /// <summary>
        /// The connections.
        /// </summary>
        private List<Connection> connections;

        /// <summary>
        /// The drag start point.
        /// </summary>
        private Point? dragStartPoint;

        // the DesignerItem this Connector belongs to;
        // retrieved from DataContext, which is set in the
        // DesignerItem template
        /// <summary>
        /// The parent designer item.
        /// </summary>
        private DesignerItem parentDesignerItem;

        /// <summary>
        /// The position.
        /// </summary>
        private Point position;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Connector"/> class.
        /// </summary>
        public Connector()
        {
            // fired when layout changes
            base.LayoutUpdated += this.Connector_LayoutUpdated;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the connections.
        /// </summary>
        public List<Connection> Connections
        {
            get
            {
                if (this.connections == null)
                {
                    this.connections = new List<Connection>();
                }

                return this.connections;
            }
        }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        public ConnectorOrientation Orientation { get; set; }

        /// <summary>
        /// Gets the parent designer item.
        /// </summary>
        public DesignerItem ParentDesignerItem
        {
            get
            {
                if (this.parentDesignerItem == null)
                {
                    this.parentDesignerItem = this.DataContext as DesignerItem;
                }

                return this.parentDesignerItem;
            }
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Point Position
        {
            get
            {
                return this.position;
            }

            set
            {
                if (this.position != value)
                {
                    this.position = value;
                    this.OnPropertyChanged("Position");
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get info.
        /// </summary>
        /// <returns>
        /// The <see cref="ConnectorInfo"/>.
        /// </returns>
        internal ConnectorInfo GetInfo()
        {
            var info = new ConnectorInfo();
            info.DesignerItemLeft = Canvas.GetLeft(this.ParentDesignerItem);
            info.DesignerItemTop = Canvas.GetTop(this.ParentDesignerItem);
            info.DesignerItemSize = new Size(this.ParentDesignerItem.ActualWidth, this.ParentDesignerItem.ActualHeight);
            info.Orientation = this.Orientation;
            info.Position = this.Position;
            return info;
        }

        // when the layout changes we update the position property

        /// <summary>
        /// The on mouse left button down.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DiagramCanvas canvas = this.GetDesignerCanvas(this);
            if (canvas != null)
            {
                // position relative to DesignerCanvas
                this.dragStartPoint = e.GetPosition(canvas);
                e.Handled = true;
            }
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
                this.dragStartPoint = null;
            }

            // but if mouse button is pressed and start point value is set we do have one
            if (this.dragStartPoint.HasValue)
            {
                // create connection adorner 
                DiagramCanvas canvas = this.GetDesignerCanvas(this);
                if (canvas != null)
                {
                    AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(canvas);
                    if (adornerLayer != null)
                    {
                        var adorner = new ConnectorAdorner(canvas, this);
                        if (adorner != null)
                        {
                            adornerLayer.Add(adorner);
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The on property changed.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// The connector_ layout updated.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Connector_LayoutUpdated(object sender, EventArgs e)
        {
            DiagramCanvas designer = this.GetDesignerCanvas(this);
            if (designer != null)
            {
                // get centre position of this Connector relative to the DesignerCanvas
                this.Position = this.TransformToAncestor(designer).Transform(new Point(this.Width / 2, this.Height / 2));
            }
        }

        /// <summary>
        /// The get designer canvas.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <returns>
        /// The <see cref="DiagramCanvas"/>.
        /// </returns>
        private DiagramCanvas GetDesignerCanvas(DependencyObject element)
        {
            while (element != null && !(element is DiagramCanvas))
            {
                element = VisualTreeHelper.GetParent(element);
            }

            return element as DiagramCanvas;
        }

        #endregion
    }

    // provides compact info about a connector; used for the 
    // routing algorithm, instead of hand over a full fledged Connector
    /// <summary>
    /// The connector info.
    /// </summary>
    internal struct ConnectorInfo
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the designer item left.
        /// </summary>
        public double DesignerItemLeft { get; set; }

        /// <summary>
        /// Gets or sets the designer item size.
        /// </summary>
        public Size DesignerItemSize { get; set; }

        /// <summary>
        /// Gets or sets the designer item top.
        /// </summary>
        public double DesignerItemTop { get; set; }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        public ConnectorOrientation Orientation { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Point Position { get; set; }

        #endregion
    }

    /// <summary>
    /// The connector orientation.
    /// </summary>
    public enum ConnectorOrientation
    {
        /// <summary>
        /// The none.
        /// </summary>
        None, 

        /// <summary>
        /// The left.
        /// </summary>
        Left, 

        /// <summary>
        /// The top.
        /// </summary>
        Top, 

        /// <summary>
        /// The right.
        /// </summary>
        Right, 

        /// <summary>
        /// The bottom.
        /// </summary>
        Bottom
    }
}