// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseMessage.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The base message.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.Common.Messages.BaseClasses
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The base message.
    /// </summary>
    public abstract class BaseMessage : IMessage
    {
        #region Fields

        /// <summary>
        /// The throw exception on failure.
        /// </summary>
        internal bool ThrowExceptionOnFailure;

        /// <summary>
        /// The _properties.
        /// </summary>
        private IDictionary<string, object> _properties = new Dictionary<string, object>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMessage"/> class.
        /// </summary>
        protected BaseMessage()
        {
            this.MessageId = Guid.NewGuid();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        public virtual string Body { get; set; }

        /// <summary>
        /// Gets or sets the message id.
        /// </summary>
        public Guid MessageId { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        public virtual IDictionary<string, object> Properties
        {
            get
            {
                return this._properties;
            }

            set
            {
                this._properties = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The clear.
        /// </summary>
        public void Clear()
        {
        }

        #endregion
    }
}