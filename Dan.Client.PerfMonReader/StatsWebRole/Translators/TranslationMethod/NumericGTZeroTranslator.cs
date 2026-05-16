// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumericGTZeroTranslator.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The numeric gt zero translator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Translators.TranslationMethod
{
    using System.Collections.Generic;

    using StatsWebRole.Exceptions;
    using StatsWebRole.Parameters;
    using StatsWebRole.Parameters.Parameter;

    /// <summary>
    /// The numeric gt zero translator.
    /// </summary>
    public class NumericGtZeroTranslator : BaseTranslator, IValueTranslator
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
            int RetVal;
            IParameter parameter = parameterCollection[parametername];
            if (int.TryParse(parameter.Value.ToString(), out RetVal))
            {
                parameter.TranslatedValue = RetVal;
                this.Results = null;
            }
            else
            {
                this.Results.Add(
                    new ErrorObject(
                        string.Format(
                            "Failed To translate Parameter [{0}] to integer: because value is incorrect [{1}] ", 
                            parameter.Name, 
                            parameter.Value)));
            }

            return this.Results;
        }

        #endregion
    }
}