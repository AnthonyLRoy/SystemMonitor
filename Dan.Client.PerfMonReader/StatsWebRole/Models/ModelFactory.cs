// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelFactory.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The model factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Models
{
    using System.Collections.Generic;
    using System.Net.Http;

    using StatsWebRole.Converters;

    /// <summary>
    /// The model factory.
    /// </summary>
    public class ModelFactory
    {
        #region Fields

        /// <summary>
        /// The _request message.
        /// </summary>
        private readonly HttpRequestMessage _requestMessage;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelFactory"/> class.
        /// </summary>
        /// <param name="requestMessage">
        /// The request message.
        /// </param>
        public ModelFactory(HttpRequestMessage requestMessage)
        {
            this._requestMessage = requestMessage;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The convert.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <param name="converter">
        /// The converter.
        /// </param>
        /// <typeparam name="TInputType">
        /// </typeparam>
        /// <typeparam name="TOutputType">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        internal IEnumerable<TOutputType> Convert<TInputType, TOutputType>(
            IEnumerable<TInputType> data, 
            IConverter<TInputType, TOutputType> converter)
        {
            return converter.ConvertEnumerable(data);
        }

        #endregion
    }
}