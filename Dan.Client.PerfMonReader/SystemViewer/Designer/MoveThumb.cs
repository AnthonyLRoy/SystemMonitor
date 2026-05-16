using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace SystemViewer.Designer
{
    public class MoveThumb : Thumb
    {
        private RotateTransform _rotateTransform;
        private ContentControl _designerItem;

        public MoveThumb()
        {
            DragStarted += new DragStartedEventHandler(this.MoveThumb_DragStarted);
            DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
        }

        private void MoveThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            this._designerItem = DataContext as ContentControl;

            if (this._designerItem != null)
            {
                this._rotateTransform = this._designerItem.RenderTransform as RotateTransform;
            }
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this._designerItem != null)
            {
                Point dragDelta = new Point(e.HorizontalChange, e.VerticalChange);

                if (this._rotateTransform != null)
                {
                    dragDelta = this._rotateTransform.Transform(dragDelta);
                }

                Canvas.SetLeft(this._designerItem, Canvas.GetLeft(this._designerItem) + dragDelta.X);
                Canvas.SetTop(this._designerItem, Canvas.GetTop(this._designerItem) + dragDelta.Y);
            }
        }
    }
}
