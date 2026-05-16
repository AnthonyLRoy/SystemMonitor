// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageCollection.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The monitor message collection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.Common.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Xml.Linq;

    using Dan.Common.Messages.Helpers;

    using Newtonsoft.Json;

    /// <summary>
    /// The monitor message collection.
    /// </summary>
    public class MonitorMessageCollection
    {
        #region Fields

        /// <summary>
        /// The _custom xml serializer.
        /// </summary>
        private readonly CustomXmlSerializer _customXmlSerializer;

        /// <summary>
        /// The _properties.
        /// </summary>
        private readonly IDictionary<string, object> _properties = new Dictionary<string, object>();

        /// <summary>
        /// The _server name.
        /// </summary>
        private readonly string _serverName;

        /// <summary>
        /// The _throwexceptiononfailure.
        /// </summary>
        private readonly bool _throwexceptiononfailure;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorMessageCollection"/> class.
        /// </summary>
        /// <param name="customXmlSerializer">
        /// The custom xml serializer.
        /// </param>
        /// <param name="serverName">
        /// The server name.
        /// </param>
        /// <param name="throwexceptiononfailure">
        /// The throwexceptiononfailure.
        /// </param>
        public MonitorMessageCollection(
            CustomXmlSerializer customXmlSerializer, 
            string serverName, 
            bool throwexceptiononfailure = false)
        {
            this._customXmlSerializer = customXmlSerializer;
            this._serverName = serverName;
            this._throwexceptiononfailure = throwexceptiononfailure;
            this.Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorMessageCollection"/> class.
        /// </summary>
        /// <param name="throwexceptiononfailure">
        /// The throwexceptiononfailure.
        /// </param>
        public MonitorMessageCollection(bool throwexceptiononfailure = false)
        {
            this._throwexceptiononfailure = throwexceptiononfailure;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        public List<MonitorMessage> Messages { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        public IDictionary<string, object> Properties { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The to compressed message.
        /// </summary>
        /// <returns>
        /// The <see cref="IMessage"/>.
        /// </returns>
        public IMessage ToCompressedMessage()
        {
            var message = new Message(this._customXmlSerializer) { Properties = this._properties, Body = this.ToJson() };
            return message;
        }

        /// <summary>
        /// The to json.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public string ToJson()
        {
            string jsonStr = null;
            try
            {
                jsonStr = JsonConvert.SerializeObject(this.Messages);
            }
            catch (Exception e)
            {
                if (this._throwexceptiononfailure)
                {
                    throw new Exception("Failed to Serialize object", e);
                }
            }

            return jsonStr;
        }

        /// <summary>
        /// The to xml.
        /// </summary>
        /// <returns>
        /// The <see cref="XElement"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public XElement ToXml()
        {
            try
            {
                return this._customXmlSerializer.ToXElement(this.Messages);
            }
            catch (Exception e)
            {
                if (this._throwexceptiononfailure)
                {
                    throw new Exception("Failed to Serialize object", e);
                }
            }

            return null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The init.
        /// </summary>
        private void Init()
        {
            string x = Helpers.GlobalSingleton.GetNextIntValue().ToString(CultureInfo.InvariantCulture);
            this.Messages = new List<MonitorMessage>();
            this._properties.Add("servermame", this._serverName);
            this._properties.Add("type", MessageType.MessageCollection.ToString());
            this._properties.Add("Sequence",x);
            Trace.WriteLine(string.Format("Published Sequenceid {0} at {1}",x, DateTime.Now));
        }

        #endregion
    }
}