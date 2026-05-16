// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMessageManager.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The MessageManager interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dan.Client.Monitor.Reader.Managers
{
    using Dan.Common.Messages;

    /// <summary>
    /// The MessageManager interface.
    /// </summary>
    public interface IMessageManager
    {
        #region Public Methods and Operators

        /// <summary>
        /// The send messages.
        /// </summary>
        /// <param name="monitorMessageCollection">
        /// The monitor message collection.
        /// </param>
        void SendMessages(MonitorMessageCollection monitorMessageCollection);

        #endregion
    }
}