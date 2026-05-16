// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterValidator.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The filter validator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Validators
{
    using StatsWebRole.Validators.Rules;

    /// <summary>
    /// The filter validator.
    /// </summary>
    public class FilterValidator : ValidatorBase, IDataValidator
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterValidator"/> class.
        /// </summary>
        public FilterValidator()
        {
            this.BuildAssociations();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The build associations.
        /// </summary>
        public void BuildAssociations()
        {
            this.ValidationRuleTable.Add(
                "filter", 
                new RuleParameterAssociation("filter", new StringIsNullorEmptyValidationRule()));
        }

        #endregion
    }
}