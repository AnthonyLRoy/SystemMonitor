// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITranslator.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The Translator interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Translators
{
    using System.Collections.Generic;

    using StatsWebRole.Exceptions;
    using StatsWebRole.Parameters;

    /// <summary>
    /// The Translator interface.
    /// </summary>
    public interface ITranslator
    {
        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether is translated.
        /// </summary>
        bool IsTranslated { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The translate.
        /// </summary>
        /// <param name="parameterCollection">
        /// The parameter collection.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<ErrorObject> Translate(ParameterCollection parameterCollection);

        #endregion
    }
}