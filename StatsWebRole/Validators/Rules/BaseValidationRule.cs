// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseValidationRule.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The base validation rule.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Validators.Rules
{
    using System.Collections.Generic;

    /// <summary>
    /// The base validation rule.
    /// </summary>
    public abstract class BaseValidationRule
    {
        #region Fields

        /// <summary>
        /// The results.
        /// </summary>
        protected List<ValidationResult> Results = new List<ValidationResult>();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether success.
        /// </summary>
        public bool Success { get; set; }

        #endregion
    }
}