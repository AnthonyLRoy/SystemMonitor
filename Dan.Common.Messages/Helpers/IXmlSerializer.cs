// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IXmlSerializer.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The XmlSerializer interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.Common.Messages.Helpers
{
    using System.IO;

    /// <summary>
    /// The XmlSerializer interface.
    /// </summary>
    public interface IXmlSerializer
    {
        #region Public Methods and Operators

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
        Stream ToXml<T>(T input) where T : class;

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
        Stream ToXml<T>(T input, string element) where T : class;

        #endregion
    }
}