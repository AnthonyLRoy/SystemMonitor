// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValueTranslator.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The ValueTranslator interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Translators.TranslationMethod
{
    using System.Collections.Generic;

    using StatsWebRole.Exceptions;
    using StatsWebRole.Parameters;

    /// <summary>
    /// The ValueTranslator interface.
    /// </summary>
    public interface IValueTranslator
    {
        #region Public Methods and Operators

        /// <summary>
        /// The translate.
        /// </summary>
        /// <param name="parametername">
        /// The parametername.
        /// </param>
        /// <param name="parameterCollection">
        /// The parameter collection.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<ErrorObject> Translate(string parametername, ParameterCollection parameterCollection);

        #endregion
    }
}