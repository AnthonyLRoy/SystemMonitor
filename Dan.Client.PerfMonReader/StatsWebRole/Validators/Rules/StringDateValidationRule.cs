// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringDateValidationRule.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The string date validation rule.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Validators.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using StatsWebRole.Common;
    using StatsWebRole.Exceptions;
    using StatsWebRole.Parameters.Parameter;

    /// <summary>
    /// The string date validation rule.
    /// </summary>
    public class StringDateValidationRule : IValidationRule
    {
        #region Fields

        /// <summary>
        /// The _results.
        /// </summary>
        private List<ValidationResult> _results = new List<ValidationResult>();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether success.
        /// </summary>
        public bool Success { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <param name="cultureInfo">
        /// The culture info.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<ValidationResult> Validate(IParameter parameter, CultureInfo cultureInfo)
        {
            if (parameter.Value == null)
            {
                this._results.Add(new ValidationResult(false, new ErrorObject(ErrorMessages.DataValueNotNull)));
            }
            else
            {
                string stringdate = parameter.Value.ToString();

                DateTime resultdate;
                if (DateTime.TryParseExact(stringdate, "yyyyMMdd", cultureInfo, DateTimeStyles.None, out resultdate))
                {
                    this.Success = true;
                    this._results.Add(new ValidationResult(true, null));
                }
                else
                {
                    this._results.Add(new ValidationResult(false, new ErrorObject(ErrorMessages.InvalidDate)));
                }
            }

            return this._results;
        }

        #endregion
    }
}