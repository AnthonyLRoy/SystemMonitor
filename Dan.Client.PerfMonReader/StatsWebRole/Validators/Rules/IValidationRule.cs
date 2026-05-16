// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidationRule.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The ValidationRule interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Validators.Rules
{
    using System.Collections.Generic;
    using System.Globalization;

    using StatsWebRole.Parameters.Parameter;

    /// <summary>
    /// The ValidationRule interface.
    /// </summary>
    public interface IValidationRule
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether success.
        /// </summary>
        bool Success { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="cultureInfo">
        /// The culture info.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<ValidationResult> Validate(IParameter value, CultureInfo cultureInfo);

        #endregion
    }
}