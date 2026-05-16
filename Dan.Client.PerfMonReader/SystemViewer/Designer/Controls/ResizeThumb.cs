// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResizeThumb.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The resize thumb.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Designer.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    using SystemViewer.Controls;

    /// <summary>
    /// The resize thumb.
    /// </summary>
    public class ResizeThumb : Thumb
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ResizeThumb"/> class.
        /// </summary>
        public ResizeThumb()
        {
            base.DragDelta += this.ResizeThumb_DragDelta;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The calculate drag limits.
        /// </summary>
        /// <param name="selectedItems">
        /// The selected items.
        /// </param>
        /// <param name="minLeft">
        /// The min left.
        /// </param>
        /// <param name="minTop">
        /// The min top.
        /// </param>
        /// <param name="minDeltaHorizontal">
        /// The min delta horizontal.
        /// </param>
        /// <param name="minDeltaVertical">
        /// The min delta vertical.
        /// </param>
        private void CalculateDragLimits(
            IEnumerable<DesignerItem> selectedItems, 
            out double minLeft, 
            out double minTop, 
            out double minDeltaHorizontal, 
            out double minDeltaVertical)
        {
            minLeft = double.MaxValue;
            minTop = double.MaxValue;
            minDeltaHorizontal = double.MaxValue;
            minDeltaVertical = double.MaxValue;

            // drag limits are set by these parameters: canvas top, canvas left, minHeight, minWidth
            // calculate min value for each parameter for each item
            foreach (var item in selectedItems)
            {
                double left = Canvas.GetLeft(item);
                double top = Canvas.GetTop(item);

                minLeft = double.IsNaN(left) ? 0 : Math.Min(left, minLeft);
                minTop = double.IsNaN(top) ? 0 : Math.Min(top, minTop);

                minDeltaVertical = Math.Min(minDeltaVertical, item.ActualHeight - item.MinHeight);
                minDeltaHorizontal = Math.Min(minDeltaHorizontal, item.ActualWidth - item.MinWidth);
            }
        }

        /// <summary>
        /// The drag bottom.
        /// </summary>
        /// <param name="scale">
        /// The scale.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="selectionService">
        /// The selection service.
        /// </param>
        private void DragBottom(double scale, DesignerItem item, SelectionService selectionService)
        {
            IEnumerable<DesignerItem> groupItems = selectionService.GetGroupMembers(item).Cast<DesignerItem>();
            double groupTop = Canvas.GetTop(item);
            foreach (var groupItem in groupItems)
            {
                double groupItemTop = Canvas.GetTop(groupItem);
                double delta = (groupItemTop - groupTop) * (scale - 1);

                Canvas.SetTop(groupItem, groupItemTop + delta);
                groupItem.Height = groupItem.ActualHeight * scale;
            }
        }

        /// <summary>
        /// The drag left.
        /// </summary>
        /// <param name="scale">
        /// The scale.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="selectionService">
        /// The selection service.
        /// </param>
        private void DragLeft(double scale, DesignerItem item, SelectionService selectionService)
        {
            IEnumerable<DesignerItem> groupItems = selectionService.GetGroupMembers(item).Cast<DesignerItem>();

            double groupLeft = Canvas.GetLeft(item) + item.Width;
            foreach (var groupItem in groupItems)
            {
                double groupItemLeft = Canvas.GetLeft(groupItem);
                double delta = (groupLeft - groupItemLeft) * (scale - 1);
                Canvas.SetLeft(groupItem, groupItemLeft - delta);
                groupItem.Width = groupItem.ActualWidth * scale;
            }
        }

        /// <summary>
        /// The drag right.
        /// </summary>
        /// <param name="scale">
        /// The scale.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="selectionService">
        /// The selection service.
        /// </param>
        private void DragRight(double scale, DesignerItem item, SelectionService selectionService)
        {
            IEnumerable<DesignerItem> groupItems = selectionService.GetGroupMembers(item).Cast<DesignerItem>();

            double groupLeft = Canvas.GetLeft(item);
            foreach (var groupItem in groupItems)
            {
                double groupItemLeft = Canvas.GetLeft(groupItem);
                double delta = (groupItemLeft - groupLeft) * (scale - 1);

                Canvas.SetLeft(groupItem, groupItemLeft + delta);
                groupItem.Width = groupItem.ActualWidth * scale;
            }
        }

        /// <summary>
        /// The drag top.
        /// </summary>
        /// <param name="scale">
        /// The scale.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="selectionService">
        /// The selection service.
        /// </param>
        private void DragTop(double scale, DesignerItem item, SelectionService selectionService)
        {
            IEnumerable<DesignerItem> groupItems = selectionService.GetGroupMembers(item).Cast<DesignerItem>();
            double groupBottom = Canvas.GetTop(item) + item.Height;
            foreach (var groupItem in groupItems)
            {
                double groupItemTop = Canvas.GetTop(groupItem);
                double delta = (groupBottom - groupItemTop) * (scale - 1);
                Canvas.SetTop(groupItem, groupItemTop - delta);
                groupItem.Height = groupItem.ActualHeight * scale;
            }
        }

        /// <summary>
        /// The resize thumb_ drag delta.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var designerItem = this.DataContext as DesignerItem;
            var designer = VisualTreeHelper.GetParent(designerItem) as DiagramCanvas;

            if (designerItem != null && designer != null && designerItem.IsSelected)
            {
                double minLeft, minTop, minDeltaHorizontal, minDeltaVertical;
                double dragDeltaVertical, dragDeltaHorizontal, scale;

                IEnumerable<DesignerItem> selectedDesignerItems =
                    designer.SelectionService.CurrentSelection.OfType<DesignerItem>();

                this.CalculateDragLimits(
                    selectedDesignerItems, 
                    out minLeft, 
                    out minTop, 
                    out minDeltaHorizontal, 
                    out minDeltaVertical);

                foreach (var item in selectedDesignerItems)
                {
                    if (item != null && item.ParentID == Guid.Empty)
                    {
                        switch (base.VerticalAlignment)
                        {
                            case VerticalAlignment.Bottom:
                                dragDeltaVertical = Math.Min(-e.VerticalChange, minDeltaVertical);
                                scale = (item.ActualHeight - dragDeltaVertical) / item.ActualHeight;
                                this.DragBottom(scale, item, designer.SelectionService);
                                break;
                            case VerticalAlignment.Top:
                                double top = Canvas.GetTop(item);
                                dragDeltaVertical = Math.Min(Math.Max(-minTop, e.VerticalChange), minDeltaVertical);
                                scale = (item.ActualHeight - dragDeltaVertical) / item.ActualHeight;
                                this.DragTop(scale, item, designer.SelectionService);
                                break;
                            default:
                                break;
                        }

                        switch (base.HorizontalAlignment)
                        {
                            case HorizontalAlignment.Left:
                                double left = Canvas.GetLeft(item);
                                dragDeltaHorizontal = Math.Min(
                                    Math.Max(-minLeft, e.HorizontalChange), 
                                    minDeltaHorizontal);
                                scale = (item.ActualWidth - dragDeltaHorizontal) / item.ActualWidth;
                                this.DragLeft(scale, item, designer.SelectionService);
                                break;
                            case HorizontalAlignment.Right:
                                dragDeltaHorizontal = Math.Min(-e.HorizontalChange, minDeltaHorizontal);
                                scale = (item.ActualWidth - dragDeltaHorizontal) / item.ActualWidth;
                                this.DragRight(scale, item, designer.SelectionService);
                                break;
                            default:
                                break;
                        }
                    }
                }

                e.Handled = true;
            }
        }

        #endregion
    }
}