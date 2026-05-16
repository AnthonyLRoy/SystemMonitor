// --------------------------------------------------------------------------------------------------------------------
// <copyright file="yyyyMMddDateTranslator.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The yyyy m mdd date translator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Translators.TranslationMethod
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using StatsWebRole.Exceptions;
    using StatsWebRole.Parameters;
    using StatsWebRole.Parameters.Parameter;

    /// <summary>
    /// The yyyy m mdd date translator.
    /// </summary>
    public class yyyyMMddDateTranslator : BaseTranslator, IValueTranslator
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
            DateTime retval;
            IParameter parameter = parameterCollection[parametername];
            if (System.DateTime.TryParseExact(
                parameter.Value.ToString(), 
                "yyyyMMdd", 
                System.Globalization.CultureInfo.InvariantCulture, 
                DateTimeStyles.None, 
                out retval))
            {
                parameter.TranslatedValue = retval;
                this.Results = null;
            }
            else
            {
                this.Results.Add(
                    new ErrorObject(
                        string.Format(
                            "Failed To translate Parameter [{0}] to date: because value is incorrect [{1}] ", 
                            parameter.Name, 
                            parameter.Value)));
            }

            return this.Results;
        }

        #endregion
    }
}