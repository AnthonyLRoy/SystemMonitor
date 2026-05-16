// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectionAdorner.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The connection adorner.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Designer.Adorners
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;

    using SystemViewer.Controls;

    /// <summary>
    /// The connection adorner.
    /// </summary>
    public class ConnectionAdorner : Adorner
    {
        #region Fields

        /// <summary>
        /// The adorner canvas.
        /// </summary>
        private Canvas adornerCanvas;

        /// <summary>
        /// The connection.
        /// </summary>
        private Connection connection;

        /// <summary>
        /// The designer canvas.
        /// </summary>
        private DiagramCanvas designerCanvas;

        /// <summary>
        /// The drag connector.
        /// </summary>
        private Connector dragConnector;

        /// <summary>
        /// The drawing pen.
        /// </summary>
        private Pen drawingPen;

        /// <summary>
        /// The fix connector.
        /// </summary>
        private Connector fixConnector;

        /// <summary>
        /// The hit connector.
        /// </summary>
        private Connector hitConnector;

        /// <summary>
        /// The hit designer item.
        /// </summary>
        private DesignerItem hitDesignerItem;

        /// <summary>
        /// The path geometry.
        /// </summary>
        private PathGeometry pathGeometry;

        /// <summary>
        /// The sink drag thumb.
        /// </summary>
        private Thumb sinkDragThumb;

        /// <summary>
        /// The source drag thumb.
        /// </summary>
        private Thumb sourceDragThumb;

        /// <summary>
        /// The visual children.
        /// </summary>
        private VisualCollection visualChildren;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionAdorner"/> class.
        /// </summary>
        /// <param name="designer">
        /// The designer.
        /// </param>
        /// <param name="connection">
        /// The connection.
        /// </param>
        public ConnectionAdorner(DiagramCanvas designer, Connection connection)
            : base(designer)
        {
            this.designerCanvas = designer;
            this.adornerCanvas = new Canvas();
            this.visualChildren = new VisualCollection(this) { this.adornerCanvas };

            this.connection = connection;
            this.connection.PropertyChanged += this.AnchorPositionChanged;

            this.InitializeDragThumbs();

            this.drawingPen = new Pen(Brushes.LightSlateGray, 1);
            this.drawingPen.LineJoin = PenLineJoin.Round;

            base.Unloaded += this.ConnectionAdorner_Unloaded;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the visual children count.
        /// </summary>
        protected override int VisualChildrenCount
        {
            get
            {
                return this.visualChildren.Count;
            }
        }

        /// <summary>
        /// Gets or sets the hit connector.
        /// </summary>
        private Connector HitConnector
        {
            get
            {
                return this.hitConnector;
            }

            set
            {
                if (this.hitConnector != value)
                {
                    this.hitConnector = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the hit designer item.
        /// </summary>
        private DesignerItem HitDesignerItem
        {
            get
            {
                return this.hitDesignerItem;
            }

            set
            {
                if (this.hitDesignerItem != value)
                {
                    if (this.hitDesignerItem != null)
                    {
                        this.hitDesignerItem.IsDragConnectionOver = false;
                    }

                    this.hitDesignerItem = value;

                    if (this.hitDesignerItem != null)
                    {
                        this.hitDesignerItem.IsDragConnectionOver = true;
                    }
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The arrange override.
        /// </summary>
        /// <param name="finalSize">
        /// The final size.
        /// </param>
        /// <returns>
        /// The <see cref="Size"/>.
        /// </returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            this.adornerCanvas.Arrange(
                new Rect(0, 0, this.designerCanvas.ActualWidth, this.designerCanvas.ActualHeight));
            return finalSize;
        }

        /// <summary>
        /// The get visual child.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="Visual"/>.
        /// </returns>
        protected override Visual GetVisualChild(int index)
        {
            return this.visualChildren[index];
        }

        /// <summary>
        /// The on render.
        /// </summary>
        /// <param name="dc">
        /// The dc.
        /// </param>
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            dc.DrawGeometry(null, this.drawingPen, this.pathGeometry);
        }

        /// <summary>
        /// The anchor position changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void AnchorPositionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("AnchorPositionSource"))
            {
                Canvas.SetLeft(this.sourceDragThumb, this.connection.AnchorPositionSource.X);
                Canvas.SetTop(this.sourceDragThumb, this.connection.AnchorPositionSource.Y);
            }

            if (e.PropertyName.Equals("AnchorPositionSink"))
            {
                Canvas.SetLeft(this.sinkDragThumb, this.connection.AnchorPositionSink.X);
                Canvas.SetTop(this.sinkDragThumb, this.connection.AnchorPositionSink.Y);
            }
        }

        /// <summary>
        /// The connection adorner_ unloaded.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ConnectionAdorner_Unloaded(object sender, RoutedEventArgs e)
        {
            this.sourceDragThumb.DragDelta -= this.thumbDragThumb_DragDelta;
            this.sourceDragThumb.DragStarted -= this.thumbDragThumb_DragStarted;
            this.sourceDragThumb.DragCompleted -= this.thumbDragThumb_DragCompleted;

            this.sinkDragThumb.DragDelta -= this.thumbDragThumb_DragDelta;
            this.sinkDragThumb.DragStarted -= this.thumbDragThumb_DragStarted;
            this.sinkDragThumb.DragCompleted -= this.thumbDragThumb_DragCompleted;
        }

        /// <summary>
        /// The hit testing.
        /// </summary>
        /// <param name="hitPoint">
        /// The hit point.
        /// </param>
        private void HitTesting(Point hitPoint)
        {
            bool hitConnectorFlag = false;

            var hitObject = this.designerCanvas.InputHitTest(hitPoint) as DependencyObject;
            while (hitObject != null && hitObject != this.fixConnector.ParentDesignerItem
                   && hitObject.GetType() != typeof(DiagramCanvas))
            {
                if (hitObject is Connector)
                {
                    this.HitConnector = hitObject as Connector;
                    hitConnectorFlag = true;
                }

                if (hitObject is DesignerItem)
                {
                    this.HitDesignerItem = hitObject as DesignerItem;
                    if (!hitConnectorFlag)
                    {
                        this.HitConnector = null;
                    }

                    return;
                }

                hitObject = VisualTreeHelper.GetParent(hitObject);
            }

            this.HitConnector = null;
            this.HitDesignerItem = null;
        }

        /// <summary>
        /// The initialize drag thumbs.
        /// </summary>
        private void InitializeDragThumbs()
        {
            var dragThumbStyle = this.connection.FindResource("ConnectionAdornerThumbStyle") as Style;

            // source drag thumb
            this.sourceDragThumb = new Thumb();
            Canvas.SetLeft(this.sourceDragThumb, this.connection.AnchorPositionSource.X);
            Canvas.SetTop(this.sourceDragThumb, this.connection.AnchorPositionSource.Y);
            this.adornerCanvas.Children.Add(this.sourceDragThumb);
            if (dragThumbStyle != null)
            {
                this.sourceDragThumb.Style = dragThumbStyle;
            }

            this.sourceDragThumb.DragDelta += this.thumbDragThumb_DragDelta;
            this.sourceDragThumb.DragStarted += this.thumbDragThumb_DragStarted;
            this.sourceDragThumb.DragCompleted += this.thumbDragThumb_DragCompleted;

            // sink drag thumb
            this.sinkDragThumb = new Thumb();
            Canvas.SetLeft(this.sinkDragThumb, this.connection.AnchorPositionSink.X);
            Canvas.SetTop(this.sinkDragThumb, this.connection.AnchorPositionSink.Y);
            this.adornerCanvas.Children.Add(this.sinkDragThumb);
            if (dragThumbStyle != null)
            {
                this.sinkDragThumb.Style = dragThumbStyle;
            }

            this.sinkDragThumb.DragDelta += this.thumbDragThumb_DragDelta;
            this.sinkDragThumb.DragStarted += this.thumbDragThumb_DragStarted;
            this.sinkDragThumb.DragCompleted += this.thumbDragThumb_DragCompleted;
        }

        /// <summary>
        /// The update path geometry.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        /// <returns>
        /// The <see cref="PathGeometry"/>.
        /// </returns>
        private PathGeometry UpdatePathGeometry(Point position)
        {
            var geometry = new PathGeometry();

            ConnectorOrientation targetOrientation;
            if (this.HitConnector != null)
            {
                targetOrientation = this.HitConnector.Orientation;
            }
            else
            {
                targetOrientation = this.dragConnector.Orientation;
            }

            List<Point> linePoints = PathFinder.GetConnectionLine(
                this.fixConnector.GetInfo(), 
                position, 
                targetOrientation);

            if (linePoints.Count > 0)
            {
                var figure = new PathFigure();
                figure.StartPoint = linePoints[0];
                linePoints.Remove(linePoints[0]);
                figure.Segments.Add(new PolyLineSegment(linePoints, true));
                geometry.Figures.Add(figure);
            }

            return geometry;
        }

        /// <summary>
        /// The thumb drag thumb_ drag completed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void thumbDragThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (this.HitConnector != null)
            {
                if (this.connection != null)
                {
                    if (this.connection.Source == this.fixConnector)
                    {
                        this.connection.Sink = this.HitConnector;
                    }
                    else
                    {
                        this.connection.Source = this.HitConnector;
                    }
                }
            }

            this.HitDesignerItem = null;
            this.HitConnector = null;
            this.pathGeometry = null;
            this.connection.StrokeDashArray = null;
            this.InvalidateVisual();
        }

        /// <summary>
        /// The thumb drag thumb_ drag delta.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void thumbDragThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Point currentPosition = Mouse.GetPosition(this);
            this.HitTesting(currentPosition);
            this.pathGeometry = this.UpdatePathGeometry(currentPosition);
            this.InvalidateVisual();
        }

        /// <summary>
        /// The thumb drag thumb_ drag started.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void thumbDragThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            this.HitDesignerItem = null;
            this.HitConnector = null;
            this.pathGeometry = null;
            this.Cursor = Cursors.Cross;
            this.connection.StrokeDashArray = new DoubleCollection(new double[] { 1, 2 });

            if (sender == this.sourceDragThumb)
            {
                this.fixConnector = this.connection.Sink;
                this.dragConnector = this.connection.Source;
            }
            else if (sender == this.sinkDragThumb)
            {
                this.dragConnector = this.connection.Sink;
                this.fixConnector = this.connection.Source;
            }
        }

        #endregion
    }
}