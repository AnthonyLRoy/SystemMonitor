// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageType.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The message type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.Common.Messages
{
    /// <summary>
    /// The message type.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// The monitor message.
        /// </summary>
        MonitorMessage = 1, 

        /// <summary>
        /// The heart beat message.
        /// </summary>
        HeartBeatMessage = 2, 

        /// <summary>
        /// The message collection.
        /// </summary>
        MessageCollection = 3
    }
}