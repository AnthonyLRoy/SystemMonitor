// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectorAdorner.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The connector adorner.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Designer.Adorners
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;

    using SystemViewer.Controls;

    /// <summary>
    /// The connector adorner.
    /// </summary>
    public class ConnectorAdorner : Adorner
    {
        #region Fields

        /// <summary>
        /// The designer canvas.
        /// </summary>
        private DiagramCanvas designerCanvas;

        /// <summary>
        /// The drawing pen.
        /// </summary>
        private Pen drawingPen;

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
        /// The source connector.
        /// </summary>
        private Connector sourceConnector;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectorAdorner"/> class.
        /// </summary>
        /// <param name="designer">
        /// The designer.
        /// </param>
        /// <param name="sourceConnector">
        /// The source connector.
        /// </param>
        public ConnectorAdorner(DiagramCanvas designer, Connector sourceConnector)
            : base(designer)
        {
            this.designerCanvas = designer;
            this.sourceConnector = sourceConnector;
            this.drawingPen = new Pen(Brushes.LightSlateGray, 1);
            this.drawingPen.LineJoin = PenLineJoin.Round;
            this.Cursor = Cursors.Cross;
        }

        #endregion

        #region Properties

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
        /// The on mouse move.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!this.IsMouseCaptured)
                {
                    this.CaptureMouse();
                }

                this.HitTesting(e.GetPosition(this));
                this.pathGeometry = this.GetPathGeometry(e.GetPosition(this));
                this.InvalidateVisual();
            }
            else
            {
                if (this.IsMouseCaptured)
                {
                    this.ReleaseMouseCapture();
                }
            }
        }

        /// <summary>
        /// The on mouse up.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (this.HitConnector != null)
            {
                Connector sourceConnector = this.sourceConnector;
                Connector sinkConnector = this.HitConnector;
                var newConnection = new Connection(sourceConnector, sinkConnector);

                Panel.SetZIndex(newConnection, this.designerCanvas.Children.Count);
                this.designerCanvas.Children.Add(newConnection);
            }

            if (this.HitDesignerItem != null)
            {
                this.HitDesignerItem.IsDragConnectionOver = false;
            }

            if (this.IsMouseCaptured)
            {
                this.ReleaseMouseCapture();
            }

            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.designerCanvas);
            if (adornerLayer != null)
            {
                adornerLayer.Remove(this);
            }
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

            // without a background the OnMouseMove event would not be fired
            // Alternative: implement a Canvas as a child of this adorner, like
            // the ConnectionAdorner does.
            dc.DrawRectangle(Brushes.Transparent, null, new Rect(this.RenderSize));
        }

        /// <summary>
        /// The get path geometry.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        /// <returns>
        /// The <see cref="PathGeometry"/>.
        /// </returns>
        private PathGeometry GetPathGeometry(Point position)
        {
            var geometry = new PathGeometry();

            ConnectorOrientation targetOrientation;
            if (this.HitConnector != null)
            {
                targetOrientation = this.HitConnector.Orientation;
            }
            else
            {
                targetOrientation = ConnectorOrientation.None;
            }

            List<Point> pathPoints = PathFinder.GetConnectionLine(
                this.sourceConnector.GetInfo(), 
                position, 
                targetOrientation);

            if (pathPoints.Count > 0)
            {
                var figure = new PathFigure();
                figure.StartPoint = pathPoints[0];
                pathPoints.Remove(pathPoints[0]);
                figure.Segments.Add(new PolyLineSegment(pathPoints, true));
                geometry.Figures.Add(figure);
            }

            return geometry;
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
            while (hitObject != null && hitObject != this.sourceConnector.ParentDesignerItem
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

        #endregion
    }
}