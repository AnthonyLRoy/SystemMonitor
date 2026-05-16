// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringListValidationRule.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The string list validation rule.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Validators.Rules
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using StatsWebRole.Common;
    using StatsWebRole.Exceptions;
    using StatsWebRole.Parameters.Parameter;

    /// <summary>
    /// The string list validation rule.
    /// </summary>
    public class StringListValidationRule : IValidationRule
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
                if (((List<string>)parameter.Value).Select(x => x.Length < 5).Any())
                {
                    this._results.Add(new ValidationResult(false, new ErrorObject(ErrorMessages.InvalidItemInList)));
                }
                else
                {
                    this.Success = true;
                    this._results.Add(new ValidationResult(true, null));
                }
            }

            return this._results;
        }

        #endregion
    }
}