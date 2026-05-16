// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DragThumb.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The drag thumb.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SystemViewer.Designer.Controls
{
    using System;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    using SystemViewer.Controls;

    /// <summary>
    /// The drag thumb.
    /// </summary>
    public class DragThumb : Thumb
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DragThumb"/> class.
        /// </summary>
        public DragThumb()
        {
            base.DragDelta += this.DragThumb_DragDelta;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The drag thumb_ drag delta.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void DragThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var designerItem = this.DataContext as DesignerItem;
            var designer = VisualTreeHelper.GetParent(designerItem) as DiagramCanvas;
            if (designerItem == null || designer == null || !designerItem.IsSelected)
            {
                return;
            }

            double minLeft = double.MaxValue;
            double minTop = double.MaxValue;

            // we only move DesignerItems
            var designerItems = designer.SelectionService.CurrentSelection.OfType<DesignerItem>();

            foreach (var item in designerItems)
            {
                var left = Canvas.GetLeft(item);
                double top = Canvas.GetTop(item);

                minLeft = double.IsNaN(left) ? 0 : Math.Min(left, minLeft);
                minTop = double.IsNaN(top) ? 0 : Math.Min(top, minTop);
            }

            var deltaHorizontal = Math.Max(-minLeft, e.HorizontalChange);
            var deltaVertical = Math.Max(-minTop, e.VerticalChange);

            foreach (var item in designerItems)
            {
                double left = Canvas.GetLeft(item);
                double top = Canvas.GetTop(item);

                if (double.IsNaN(left))
                {
                    left = 0;
                }

                if (double.IsNaN(top))
                {
                    top = 0;
                }

                Canvas.SetLeft(item, left + deltaHorizontal);
                Canvas.SetTop(item, top + deltaVertical);
            }

            designer.InvalidateMeasure();
            e.Handled = true;
        }

        #endregion
    }
}