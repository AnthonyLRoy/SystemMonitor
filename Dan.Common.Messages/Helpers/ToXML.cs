// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ToXML.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The custom xml serializer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.Common.Messages.Helpers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// The custom xml serializer.
    /// </summary>
    public class CustomXmlSerializer : IXmlSerializer
    {
        #region Constants

        /// <summary>
        /// The default element name.
        /// </summary>
        private const string DefaultElementName = "object";

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Converts an object of type 
        /// <typeparam name="T">
        /// </typeparam>
        /// to an XElement.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// Returns the object as it's XML representation in an XElement.
        /// </returns>
        public XElement ToXElement<T>(T input) where T : class
        {
            return this.ToXElement(input, null);
        }

        /// <summary>
        /// Converts an object of type 
        /// <typeparam name="T">
        /// </typeparam>
        /// to an XElement.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="element">
        /// The element name.
        /// </param>
        /// <returns>
        /// Returns the object as it's XML representation in an XElement.
        /// </returns>
        public XElement ToXElement<T>(T input, string element) where T : class
        {
            return this.ToXElementInternal(input, element);
        }

        /// <summary>
        /// Generates an XML representation of an object of type 
        /// <typeparam name="T">
        /// </typeparam>
        /// and returns a <see cref="Stream"/> containing the result.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// A <see cref="Stream"/> with the resulting XML.
        /// </returns>
        public Stream ToXml<T>(T input) where T : class
        {
            return this.ToXml(input, null);
        }

        /// <summary>
        /// Generates an XML representation of an object of type 
        /// <typeparam name="T">
        /// </typeparam>
        /// and returns a <see cref="Stream"/> containing the result.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="element">
        /// The root element name.
        /// </param>
        /// <returns>
        /// A <see cref="Stream"/> with the resulting XML.
        /// </returns>
        public Stream ToXml<T>(T input, string element) where T : class
        {
            var xmlElement = this.ToXElement(input, element);

            var outputStream = new MemoryStream();
            if (xmlElement != null)
            {
                xmlElement.Save(outputStream);
            }

            return outputStream;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get properties.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private static IEnumerable<KeyValuePair<string, object>> GetProperties(object input)
        {
            var type = input.GetType();

            return
                type.GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public)
                    .ToDictionary(x => x.Name, x => x.GetValue(input, null));
        }

        /// <summary>
        /// The serialize enumerable.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <returns>
        /// The <see cref="XElement"/>.
        /// </returns>
        private XElement SerializeEnumerable(IEnumerable input, string element)
        {
            var rootElement = new XElement(element);

            var enumerator = input.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var value = enumerator.Current;
                XElement childElement = TypeHelper.IsSimpleOrNullableType(value.GetType())
                                            ? new XElement(element + "Item", value)
                                            : this.ToXElementInternal(value, element + "Item");

                rootElement.Add(childElement);
            }

            return rootElement;
        }

        /// <summary>
        /// The to x element internal.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <returns>
        /// The <see cref="XElement"/>.
        /// </returns>
        private XElement ToXElementInternal(object input, string element)
        {
            if (input == null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(element))
            {
                element = DefaultElementName;
            }

            element = XmlConvert.EncodeName(element);

            var type = input.GetType();

            if (TypeHelper.IsEnumerable(type))
            {
                return this.SerializeEnumerable((IEnumerable)input, element);
            }

            var xElement = new XElement(element);

            var props = GetProperties(input);
            var elements =
                props.Where(x => x.Value != null)
                    .Select(
                        x =>
                        TypeHelper.IsSimpleOrNullableType(x.Value.GetType())
                            ? new XElement(x.Key, x.Value)
                            : this.ToXElementInternal(x.Value, x.Key))
                    .Where(x => x != null);

            xElement.Add(elements);

            return xElement;
        }

        #endregion
    }
}