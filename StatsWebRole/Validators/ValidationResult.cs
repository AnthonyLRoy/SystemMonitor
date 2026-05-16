// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationResult.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The validation result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Validators
{
    using StatsWebRole.Exceptions;

    /// <summary>
    /// The validation result.
    /// </summary>
    public class ValidationResult : IValidationResult
    {
        #region Fields

        /// <summary>
        /// The errors.
        /// </summary>
        public readonly ErrorObject Errors;

        /// <summary>
        /// The result.
        /// </summary>
        public readonly bool Result;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult"/> class.
        /// </summary>
        public ValidationResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult"/> class.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <param name="errors">
        /// The errors.
        /// </param>
        public ValidationResult(bool result, ErrorObject errors)
        {
            this.Result = result;
            this.Errors = errors;
        }

        #endregion
    }
}