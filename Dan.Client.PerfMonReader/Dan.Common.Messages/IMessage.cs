// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMessage.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The Message interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.Common.Messages
{
    using System.Collections.Generic;

    /// <summary>
    /// The Message interface.
    /// </summary>
    public interface IMessage
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        string Body { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        IDictionary<string, object> Properties { get; set; }

        #endregion
    }
}