// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelativePositionPanel.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The relative position panel.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Designer.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// The relative position panel.
    /// </summary>
    public class RelativePositionPanel : Panel
    {
        #region Static Fields

        /// <summary>
        /// The relative position property.
        /// </summary>
        public static readonly DependencyProperty RelativePositionProperty =
            DependencyProperty.RegisterAttached(
                "RelativePosition", 
                typeof(Point), 
                typeof(RelativePositionPanel), 
                new FrameworkPropertyMetadata(new Point(0, 0), RelativePositionPanel.OnRelativePositionChanged));

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get relative position.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static Point GetRelativePosition(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            return (Point)element.GetValue(RelativePositionProperty);
        }

        /// <summary>
        /// The set relative position.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static void SetRelativePosition(UIElement element, Point value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            element.SetValue(RelativePositionProperty, value);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The arrange override.
        /// </summary>
        /// <param name="arrangeSize">
        /// The arrange size.
        /// </param>
        /// <returns>
        /// The <see cref="Size"/>.
        /// </returns>
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            foreach (UIElement element in base.InternalChildren)
            {
                if (element != null)
                {
                    Point relPosition = GetRelativePosition(element);
                    double x = (arrangeSize.Width - element.DesiredSize.Width) * relPosition.X;
                    double y = (arrangeSize.Height - element.DesiredSize.Height) * relPosition.Y;

                    if (double.IsNaN(x))
                    {
                        x = 0;
                    }

                    if (double.IsNaN(y))
                    {
                        y = 0;
                    }

                    element.Arrange(new Rect(new Point(x, y), element.DesiredSize));
                }
            }

            return arrangeSize;
        }

        /// <summary>
        /// The measure override.
        /// </summary>
        /// <param name="availableSize">
        /// The available size.
        /// </param>
        /// <returns>
        /// The <see cref="Size"/>.
        /// </returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            var size = new Size(double.PositiveInfinity, double.PositiveInfinity);

            // SDK docu says about InternalChildren Property: 'Classes that are derived from Panel 
            // should use this property, instead of the Children property, for internal overrides 
            // such as MeasureCore and ArrangeCore.
            foreach (UIElement element in this.InternalChildren)
            {
                if (element != null)
                {
                    element.Measure(size);
                }
            }

            return base.MeasureOverride(availableSize);
        }

        /// <summary>
        /// The on relative position changed.
        /// </summary>
        /// <param name="d">
        /// The d.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void OnRelativePositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var reference = d as UIElement;
            if (reference != null)
            {
                var parent = VisualTreeHelper.GetParent(reference) as RelativePositionPanel;
                if (parent != null)
                {
                    parent.InvalidateArrange();
                }
            }
        }

        #endregion
    }
}