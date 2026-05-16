// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Connection.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The connection.
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
    /// The connection.
    /// </summary>
    public class Connection : Control, ISelectable, INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The sink arrow symbol.
        /// </summary>
        public ArrowSymbol sinkArrowSymbol = ArrowSymbol.Arrow;

        /// <summary>
        /// The anchor angle sink.
        /// </summary>
        private double anchorAngleSink;

        /// <summary>
        /// The anchor angle source.
        /// </summary>
        private double anchorAngleSource;

        /// <summary>
        /// The anchor position sink.
        /// </summary>
        private Point anchorPositionSink;

        /// <summary>
        /// The anchor position source.
        /// </summary>
        private Point anchorPositionSource;

        /// <summary>
        /// The connection adorner.
        /// </summary>
        private Adorner connectionAdorner;

        /// <summary>
        /// The is selected.
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// The label position.
        /// </summary>
        private Point labelPosition;

        // connection path geometry
        /// <summary>
        /// The path geometry.
        /// </summary>
        private PathGeometry pathGeometry;

        /// <summary>
        /// The sink.
        /// </summary>
        private Connector sink;

        /// <summary>
        /// The source.
        /// </summary>
        private Connector source;

        /// <summary>
        /// The source arrow symbol.
        /// </summary>
        private ArrowSymbol sourceArrowSymbol = ArrowSymbol.None;

        /// <summary>
        /// The stroke dash array.
        /// </summary>
        private DoubleCollection strokeDashArray;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="sink">
        /// The sink.
        /// </param>
        public Connection(Connector source, Connector sink)
        {
            this.ID = Guid.NewGuid();
            this.Source = source;
            this.Sink = sink;
            base.Unloaded += this.Connection_Unloaded;
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
        /// Gets or sets the anchor angle sink.
        /// </summary>
        public double AnchorAngleSink
        {
            get
            {
                return this.anchorAngleSink;
            }

            set
            {
                if (this.anchorAngleSink != value)
                {
                    this.anchorAngleSink = value;
                    this.OnPropertyChanged("AnchorAngleSink");
                }
            }
        }

        /// <summary>
        /// Gets or sets the anchor angle source.
        /// </summary>
        public double AnchorAngleSource
        {
            get
            {
                return this.anchorAngleSource;
            }

            set
            {
                if (this.anchorAngleSource != value)
                {
                    this.anchorAngleSource = value;
                    this.OnPropertyChanged("AnchorAngleSource");
                }
            }
        }

        // analogue to source side

        /// <summary>
        /// Gets or sets the anchor position sink.
        /// </summary>
        public Point AnchorPositionSink
        {
            get
            {
                return this.anchorPositionSink;
            }

            set
            {
                if (this.anchorPositionSink != value)
                {
                    this.anchorPositionSink = value;
                    this.OnPropertyChanged("AnchorPositionSink");
                }
            }
        }

        /// <summary>
        /// Gets or sets the anchor position source.
        /// </summary>
        public Point AnchorPositionSource
        {
            get
            {
                return this.anchorPositionSource;
            }

            set
            {
                if (this.anchorPositionSource != value)
                {
                    this.anchorPositionSource = value;
                    this.OnPropertyChanged("AnchorPositionSource");
                }
            }
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is selected.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                    if (this.isSelected)
                    {
                        this.ShowAdorner();
                    }
                    else
                    {
                        this.HideAdorner();
                    }
                }
            }
        }

        // analogue to source side

        /// <summary>
        /// Gets or sets the label position.
        /// </summary>
        public Point LabelPosition
        {
            get
            {
                return this.labelPosition;
            }

            set
            {
                if (this.labelPosition != value)
                {
                    this.labelPosition = value;
                    this.OnPropertyChanged("LabelPosition");
                }
            }
        }

        /// <summary>
        /// Gets or sets the path geometry.
        /// </summary>
        public PathGeometry PathGeometry
        {
            get
            {
                return this.pathGeometry;
            }

            set
            {
                if (this.pathGeometry != value)
                {
                    this.pathGeometry = value;
                    this.UpdateAnchorPosition();
                    this.OnPropertyChanged("PathGeometry");
                }
            }
        }

        /// <summary>
        /// Gets or sets the sink.
        /// </summary>
        public Connector Sink
        {
            get
            {
                return this.sink;
            }

            set
            {
                if (this.sink != value)
                {
                    if (this.sink != null)
                    {
                        this.sink.PropertyChanged -= this.OnConnectorPositionChanged;
                        this.sink.Connections.Remove(this);
                    }

                    this.sink = value;

                    if (this.sink != null)
                    {
                        this.sink.Connections.Add(this);
                        this.sink.PropertyChanged += this.OnConnectorPositionChanged;
                    }

                    this.UpdatePathGeometry();
                }
            }
        }

        /// <summary>
        /// Gets or sets the sink arrow symbol.
        /// </summary>
        public ArrowSymbol SinkArrowSymbol
        {
            get
            {
                return this.sinkArrowSymbol;
            }

            set
            {
                if (this.sinkArrowSymbol != value)
                {
                    this.sinkArrowSymbol = value;
                    this.OnPropertyChanged("SinkArrowSymbol");
                }
            }
        }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        public Connector Source
        {
            get
            {
                return this.source;
            }

            set
            {
                if (this.source != value)
                {
                    if (this.source != null)
                    {
                        this.source.PropertyChanged -= this.OnConnectorPositionChanged;
                        this.source.Connections.Remove(this);
                    }

                    this.source = value;

                    if (this.source != null)
                    {
                        this.source.Connections.Add(this);
                        this.source.PropertyChanged += this.OnConnectorPositionChanged;
                    }

                    this.UpdatePathGeometry();
                }
            }
        }

        /// <summary>
        /// Gets or sets the source arrow symbol.
        /// </summary>
        public ArrowSymbol SourceArrowSymbol
        {
            get
            {
                return this.sourceArrowSymbol;
            }

            set
            {
                if (this.sourceArrowSymbol != value)
                {
                    this.sourceArrowSymbol = value;
                    this.OnPropertyChanged("SourceArrowSymbol");
                }
            }
        }

        // pattern of dashes and gaps that is used to outline the connection path

        /// <summary>
        /// Gets or sets the stroke dash array.
        /// </summary>
        public DoubleCollection StrokeDashArray
        {
            get
            {
                return this.strokeDashArray;
            }

            set
            {
                if (this.strokeDashArray != value)
                {
                    this.strokeDashArray = value;
                    this.OnPropertyChanged("StrokeDashArray");
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The hide adorner.
        /// </summary>
        internal void HideAdorner()
        {
            if (this.connectionAdorner != null)
            {
                this.connectionAdorner.Visibility = Visibility.Collapsed;
            }
        }

        // if connected, the ConnectionAdorner becomes visible

        /// <summary>
        /// The on mouse down.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            // usual selection business
            var designer = VisualTreeHelper.GetParent(this) as DiagramCanvas;
            if (designer != null)
            {
                if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
                {
                    if (this.IsSelected)
                    {
                        designer.SelectionService.RemoveFromSelection(this);
                    }
                    else
                    {
                        designer.SelectionService.AddToSelection(this);
                    }
                }
                else if (!this.IsSelected)
                {
                    designer.SelectionService.SelectItem(this);
                }

                this.Focus();
            }

            e.Handled = false;
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
        /// The connection_ unloaded.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Connection_Unloaded(object sender, RoutedEventArgs e)
        {
            // do some housekeeping when Connection is unloaded

            // remove event handler
            this.Source = null;
            this.Sink = null;

            // remove adorner
            if (this.connectionAdorner != null)
            {
                var designer = VisualTreeHelper.GetParent(this) as DiagramCanvas;

                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    adornerLayer.Remove(this.connectionAdorner);
                    this.connectionAdorner = null;
                }
            }
        }

        /// <summary>
        /// The on connector position changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnConnectorPositionChanged(object sender, PropertyChangedEventArgs e)
        {
            // whenever the 'Position' property of the source or sink Connector 
            // changes we must update the connection path geometry
            if (e.PropertyName.Equals("Position"))
            {
                this.UpdatePathGeometry();
            }
        }

        /// <summary>
        /// The show adorner.
        /// </summary>
        private void ShowAdorner()
        {
            // the ConnectionAdorner is created once for each Connection
            if (this.connectionAdorner == null)
            {
                var designer = VisualTreeHelper.GetParent(this) as DiagramCanvas;

                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    this.connectionAdorner = new ConnectionAdorner(designer, this);
                    adornerLayer.Add(this.connectionAdorner);
                }
            }

            this.connectionAdorner.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// The update anchor position.
        /// </summary>
        private void UpdateAnchorPosition()
        {
            Point pathStartPoint, pathTangentAtStartPoint;
            Point pathEndPoint, pathTangentAtEndPoint;
            Point pathMidPoint, pathTangentAtMidPoint;

            // the PathGeometry.GetPointAtFractionLength method gets the point and a tangent vector 
            // on PathGeometry at the specified fraction of its length
            this.PathGeometry.GetPointAtFractionLength(0, out pathStartPoint, out pathTangentAtStartPoint);
            this.PathGeometry.GetPointAtFractionLength(1, out pathEndPoint, out pathTangentAtEndPoint);
            this.PathGeometry.GetPointAtFractionLength(0.5, out pathMidPoint, out pathTangentAtMidPoint);

            // get angle from tangent vector
            this.AnchorAngleSource = Math.Atan2(-pathTangentAtStartPoint.Y, -pathTangentAtStartPoint.X)
                                     * (180 / Math.PI);
            this.AnchorAngleSink = Math.Atan2(pathTangentAtEndPoint.Y, pathTangentAtEndPoint.X) * (180 / Math.PI);

            // add some margin on source and sink side for visual reasons only
            pathStartPoint.Offset(-pathTangentAtStartPoint.X * 5, -pathTangentAtStartPoint.Y * 5);
            pathEndPoint.Offset(pathTangentAtEndPoint.X * 5, pathTangentAtEndPoint.Y * 5);

            this.AnchorPositionSource = pathStartPoint;
            this.AnchorPositionSink = pathEndPoint;
            this.LabelPosition = pathMidPoint;
        }

        /// <summary>
        /// The update path geometry.
        /// </summary>
        private void UpdatePathGeometry()
        {
            if (this.Source != null && this.Sink != null)
            {
                var geometry = new PathGeometry();
                List<Point> linePoints = PathFinder.GetConnectionLine(this.Source.GetInfo(), this.Sink.GetInfo(), true);
                if (linePoints.Count > 0)
                {
                    var figure = new PathFigure();
                    figure.StartPoint = linePoints[0];
                    linePoints.Remove(linePoints[0]);
                    figure.Segments.Add(new PolyLineSegment(linePoints, true));
                    geometry.Figures.Add(figure);

                    this.PathGeometry = geometry;
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// The arrow symbol.
    /// </summary>
    public enum ArrowSymbol
    {
        /// <summary>
        /// The none.
        /// </summary>
        None, 

        /// <summary>
        /// The arrow.
        /// </summary>
        Arrow, 

        /// <summary>
        /// The diamond.
        /// </summary>
        Diamond
    }
}