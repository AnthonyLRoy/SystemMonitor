// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGroupable.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The Groupable interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer
{
    using System;

    /// <summary>
    /// The Groupable interface.
    /// </summary>
    public interface IGroupable
    {
        #region Public Properties

        /// <summary>
        /// Gets the id.
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// Gets or sets a value indicating whether is group.
        /// </summary>
        bool IsGroup { get; set; }

        /// <summary>
        /// Gets or sets the parent id.
        /// </summary>
        Guid ParentID { get; set; }

        #endregion
    }
}