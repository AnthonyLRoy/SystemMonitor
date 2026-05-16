// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlManager.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The control manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Managers
{
    using System.Windows;

    using SystemViewer.Models.ToolBox;

    /// <summary>
    /// The control manager.
    /// </summary>
    public static class ControlManager
    {
        #region Methods

        /// <summary>
        /// The create control.
        /// </summary>
        /// <param name="toolBoxItem">
        /// The tool box item.
        /// </param>
        /// <returns>
        /// The <see cref="UIElement"/>.
        /// </returns>
        internal static UIElement CreateControl(DiagramToolBoxItem toolBoxItem)
        {
            var result = (UIElement)toolBoxItem.Library.CreateInstance(toolBoxItem.Fullname);

            return result;
        }

        #endregion
    }
}