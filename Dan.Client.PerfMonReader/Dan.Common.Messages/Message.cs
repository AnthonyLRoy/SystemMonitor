// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Message.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The message.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.Common.Messages
{
    using Dan.Common.Messages.BaseClasses;
    using Dan.Common.Messages.Helpers;

    /// <summary>
    /// The message.
    /// </summary>
    public class Message : BaseMessage
    {
        #region Fields

        /// <summary>
        /// The _custom xml serializer.
        /// </summary>
        protected readonly CustomXmlSerializer _customXmlSerializer;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="customXmlSerializer">
        /// The custom xml serializer.
        /// </param>
        public Message(CustomXmlSerializer customXmlSerializer)
        {
            this._customXmlSerializer = customXmlSerializer;
        }

        #endregion
    }
}