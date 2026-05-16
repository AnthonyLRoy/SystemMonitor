// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoTranslationTranslator.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The no translation translator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Translators.TranslationMethod
{
    using System.Collections.Generic;

    using StatsWebRole.Exceptions;
    using StatsWebRole.Parameters;
    using StatsWebRole.Parameters.Parameter;

    /// <summary>
    /// The no translation translator.
    /// </summary>
    public class NoTranslationTranslator : BaseTranslator, IValueTranslator
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
        public List<ErrorObject> Translate(string parametername, ParameterCollection parameterCollection)
        {
            IParameter parameter = parameterCollection[parametername];

            parameter.TranslatedValue = parameter.Value;
            this.Results = null;

            return this.Results;
        }

        #endregion
    }
}