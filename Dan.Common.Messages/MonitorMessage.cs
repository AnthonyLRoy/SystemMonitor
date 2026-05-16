// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonitorMessage.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The monitor message.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.Common.Messages
{
    using Dan.Common.Messages.Helpers;

    /// <summary>
    /// The monitor message.
    /// </summary>
    public class MonitorMessage : Message
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorMessage"/> class.
        /// </summary>
        /// <param name="customXmlSerializer">
        /// The custom xml serializer.
        /// </param>
        public MonitorMessage(CustomXmlSerializer customXmlSerializer)
            : base(customXmlSerializer)
        {
            this.Init();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The init.
        /// </summary>
        private void Init()
        {
            this.Properties.Add("messageType", MessageType.MonitorMessage.ToString());
        }

        #endregion
    }
}