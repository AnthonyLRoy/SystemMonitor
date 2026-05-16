// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumericGtZeroValidationRule.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   NumericGtZeroValidationRule.cs
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Validators.Rules
{
    namespace Stats.Web.Api.Validators.Rules
    {
        using System.Collections.Generic;
        using System.Globalization;

        using StatsWebRole.Exceptions;
        using StatsWebRole.Parameters.Parameter;

        public class NumericGtZeroValidationRule : IValidationRule
        {
            #region Fields

            private readonly List<ValidationResult> _results = new List<ValidationResult>();

            private int _validResult = -1;

            #endregion

            #region Public Properties

            public bool Success { get; set; }

            #endregion

            #region Public Methods and Operators

            public List<ValidationResult> Validate(IParameter parameter, CultureInfo cultureInfo)
            {
                if (parameter == null || parameter.Value == null)
                {
                    this._results.Add(new ValidationResult(false, new ErrorObject("value must not be null")));
                }
                else
                {
                    if (int.TryParse(parameter.Value.ToString(), out this._validResult))
                    {
                        this._results.Add(
                            this._validResult > 0
                                ? new ValidationResult(true, null)
                                : new ValidationResult(false, new ErrorObject("Value must be greater that Zero")));
                    }
                    else
                    {
                        this._results.Add(new ValidationResult(false, new ErrorObject("Value must be an integer")));
                    }
                }

                if (this._results.Count == 1 && this._results[0].Errors == null)
                {
                    this.Success = true;
                }

                return this._results;
            }

            #endregion
        }
    }
}