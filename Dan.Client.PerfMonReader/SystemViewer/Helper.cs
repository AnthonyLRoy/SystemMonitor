// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Helper.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    using SystemViewer.Controls;
    using SystemViewer.Forms;

    using Dan.monitor.Common;

    /// <summary>
    /// The helper.
    /// </summary>
    public static class Helper
    {
        #region Methods

        /// <summary>
        /// The get mainwindow.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <returns>
        /// The <see cref="MainWindow"/>.
        /// </returns>
        internal static MainWindow GetMainwindow(DependencyObject sender)
        {
            var parent = VisualTreeHelper.GetParent(sender);
            while (!(parent is MainWindow))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return (MainWindow)parent;
        }



        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}

#endregion

