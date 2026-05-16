// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceStatsListValidator.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The service stats list validator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Validators
{
    using StatsWebRole.Validators.Rules;

    /// <summary>
    /// The service stats list validator.
    /// </summary>
    public class ServiceStatsListValidator : ValidatorBase, IDataValidator
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceStatsListValidator"/> class.
        /// </summary>
        public ServiceStatsListValidator()
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
                "requestList", 
                new RuleParameterAssociation("requestList", new StringListValidationRule()));
        }

        #endregion
    }
}