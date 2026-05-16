// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ToolBoxItem.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The diagram tool box item.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Models.ToolBox
{
    using System;
    using System.Drawing;
    using System.Reflection;

    /// <summary>
    /// The diagram tool box item.
    /// </summary>
    public class DiagramToolBoxItem
    {
        #region Fields

        /// <summary>
        /// The is active.
        /// </summary>
        public bool IsActive;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the fullname.
        /// </summary>
        public string Fullname { get; set; }

        /// <summary>
        /// Gets or sets the icon image.
        /// </summary>
        public Uri IconImage { get; set; }

        /// <summary>
        /// Gets or sets the library.
        /// </summary>
        public Assembly Library { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}