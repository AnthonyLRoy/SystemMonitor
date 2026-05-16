// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorObject.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The error object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Exceptions
{
    using StatsWebRole.Common;

    /// <summary>
    /// The error object.
    /// </summary>
    public class ErrorObject
    {
        #region Fields

        /// <summary>
        /// The _message.
        /// </summary>
        private readonly string _message;

        /// <summary>
        /// The _message type.
        /// </summary>
        private readonly MessageType _messageType;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorObject"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="messageType">
        /// The message type.
        /// </param>
        public ErrorObject(string message, MessageType messageType = MessageType.Info)
        {
            this._message = message;
            this._messageType = messageType;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message
        {
            get
            {
                return this._message;
            }
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        public MessageType Status
        {
            get
            {
                return this._messageType;
            }
        }

        #endregion
    }
}