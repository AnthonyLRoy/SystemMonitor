// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RuleParameterAssociation.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The rule parameter association.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Validators
{
    using StatsWebRole.Validators.Rules;

    /// <summary>
    /// The rule parameter association.
    /// </summary>
    public class RuleParameterAssociation
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleParameterAssociation"/> class.
        /// </summary>
        /// <param name="parametername">
        /// The parametername.
        /// </param>
        /// <param name="rule">
        /// The rule.
        /// </param>
        public RuleParameterAssociation(string parametername, IValidationRule rule)
        {
            this.Parametername = parametername;
            this.Rule = rule;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the parametername.
        /// </summary>
        public string Parametername { get; set; }

        /// <summary>
        /// Gets or sets the rule.
        /// </summary>
        public IValidationRule Rule { get; set; }

        #endregion
    }
}