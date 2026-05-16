// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISelectable.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The Selectable interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer
{
    // Common interface for items that can be selected
    // on the DesignerCanvas; used by DesignerItem and Connection
    /// <summary>
    /// The Selectable interface.
    /// </summary>
    public interface ISelectable
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether is selected.
        /// </summary>
        bool IsSelected { get; set; }

        #endregion
    }
}