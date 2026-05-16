// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RubberbandAdorner.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The rubberband adorner.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Designer.Adorners
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;

    using SystemViewer.Controls;

    /// <summary>
    /// The rubberband adorner.
    /// </summary>
    public class RubberbandAdorner : Adorner
    {
        #region Fields

        /// <summary>
        /// The designer canvas.
        /// </summary>
        private DiagramCanvas designerCanvas;

        /// <summary>
        /// The end point.
        /// </summary>
        private Point? endPoint;

        /// <summary>
        /// The rubberband pen.
        /// </summary>
        private Pen rubberbandPen;

        /// <summary>
        /// The start point.
        /// </summary>
        private Point? startPoint;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RubberbandAdorner"/> class.
        /// </summary>
        /// <param name="designerCanvas">
        /// The designer canvas.
        /// </param>
        /// <param name="dragStartPoint">
        /// The drag start point.
        /// </param>
        public RubberbandAdorner(DiagramCanvas designerCanvas, Point? dragStartPoint)
            : base(designerCanvas)
        {
            this.designerCanvas = designerCanvas;
            this.startPoint = dragStartPoint;
            this.rubberbandPen = new Pen(Brushes.LightSlateGray, 1);
            this.rubberbandPen.DashStyle = new DashStyle(new double[] { 2 }, 1);
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

                this.endPoint = e.GetPosition(this);
                this.UpdateSelection();
                this.InvalidateVisual();
            }
            else
            {
                if (this.IsMouseCaptured)
                {
                    this.ReleaseMouseCapture();
                }
            }

            e.Handled = true;
        }

        /// <summary>
        /// The on mouse up.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            // release mouse capture
            if (this.IsMouseCaptured)
            {
                this.ReleaseMouseCapture();
            }

            // remove this adorner from adorner layer
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.designerCanvas);
            if (adornerLayer != null)
            {
                adornerLayer.Remove(this);
            }

            e.Handled = true;
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

            // without a background the OnMouseMove event would not be fired!
            // Alternative: implement a Canvas as a child of this adorner, like
            // the ConnectionAdorner does.
            dc.DrawRectangle(Brushes.Transparent, null, new Rect(this.RenderSize));

            if (this.startPoint.HasValue && this.endPoint.HasValue)
            {
                dc.DrawRectangle(
                    Brushes.Transparent, 
                    this.rubberbandPen, 
                    new Rect(this.startPoint.Value, this.endPoint.Value));
            }
        }

        /// <summary>
        /// The update selection.
        /// </summary>
        private void UpdateSelection()
        {
            this.designerCanvas.SelectionService.ClearSelection();

            var rubberBand = new Rect(this.startPoint.Value, this.endPoint.Value);
            foreach (Control item in this.designerCanvas.Children)
            {
                Rect itemRect = VisualTreeHelper.GetDescendantBounds(item);
                Rect itemBounds = item.TransformToAncestor(this.designerCanvas).TransformBounds(itemRect);

                if (rubberBand.Contains(itemBounds))
                {
                    if (item is Connection)
                    {
                        this.designerCanvas.SelectionService.AddToSelection(item as ISelectable);
                    }
                    else
                    {
                        var di = item as DesignerItem;
                        if (di.ParentID == Guid.Empty)
                        {
                            this.designerCanvas.SelectionService.AddToSelection(di);
                        }
                    }
                }
            }
        }

        #endregion
    }
}