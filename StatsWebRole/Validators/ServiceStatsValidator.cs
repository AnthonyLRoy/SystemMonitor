// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceStatsValidator.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The service stats validator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Validators
{
    using StatsWebRole.Validators.Rules;

    /// <summary>
    /// The service stats validator.
    /// </summary>
    public class ServiceStatsValidator : ValidatorBase, IDataValidator
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceStatsValidator"/> class.
        /// </summary>
        public ServiceStatsValidator()
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
                "groupName", 
                new RuleParameterAssociation("groupName", new StringIsNullorEmptyValidationRule()));
        }

        #endregion
    }
}