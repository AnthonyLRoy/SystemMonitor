// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMessageService.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The MessageService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ServiceBusClient
{
    using System.Collections.Generic;

    using Dan.Common.Messages;

    /// <summary>
    /// The MessageService interface.
    /// </summary>
    public interface IMessageService
    {
        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether is connection active.
        /// </summary>
        bool IsConnectionActive { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The publish.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Publish(IMessage message);

        /// <summary>
        /// The publish.
        /// </summary>
        /// <param name="messages">
        /// The messages.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Publish(List<IMessage> messages);

        #endregion
    }
}