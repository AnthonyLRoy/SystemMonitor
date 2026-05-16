// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowsToolbox.xaml.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   Interaction logic for toolbox.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using SystemViewer.Models.ToolBox;

    /// <summary>
    /// Interaction logic for toolbox.xaml
    /// </summary>
    public partial class Windowtoolbox : UserControl
    {
        #region Fields

        /// <summary>
        /// The _start point.
        /// </summary>
        private Point _startPoint;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Windowtoolbox"/> class.
        /// </summary>
        public Windowtoolbox()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The list_ mouse move.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void List_MouseMove(object sender, MouseEventArgs e)
        {
            var toolboxitems = sender as ListBox;

            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = this._startPoint - mousePos;

            if (e.LeftButton != MouseButtonState.Pressed
                || (!(Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance)
                    && !(Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)))
            {
                return;
            }

            if (toolboxitems.SelectedItem == null)
            {
                return;
            }

            var dragData = new DataObject("toolboxitem", toolboxitems.SelectedItem as DiagramToolBoxItem);
            DragDrop.DoDragDrop(this, dragData, DragDropEffects.Copy);
        }

        /// <summary>
        /// The list_ preview mouse left button down.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void List_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Store the mouse position
            this._startPoint = e.GetPosition(null);
        }

        #endregion
    }
}