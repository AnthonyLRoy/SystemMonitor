// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringIsNullorEmptyValidationRule.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The string is nullor empty validation rule.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Validators.Rules
{
    using System.Collections.Generic;
    using System.Globalization;

    using StatsWebRole.Common;
    using StatsWebRole.Exceptions;
    using StatsWebRole.Parameters.Parameter;

    /// <summary>
    /// The string is nullor empty validation rule.
    /// </summary>
    public class StringIsNullorEmptyValidationRule : BaseValidationRule, IValidationRule
    {
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
                this.Results.Add(new ValidationResult(false, new ErrorObject(ErrorMessages.DataValueNotNull)));
            }
            else
            {
                string serviceName = parameter.Value.ToString();

                // Validate here 
                if (!string.IsNullOrEmpty(serviceName))
                {
                    this.Success = true;
                    this.Results.Add(new ValidationResult(true, null));
                }
                else
                {
                    this.Results.Add(new ValidationResult(false, new ErrorObject(ErrorMessages.InvalidStringParameter)));
                }
            }

            return this.Results;
        }

        #endregion
    }
}