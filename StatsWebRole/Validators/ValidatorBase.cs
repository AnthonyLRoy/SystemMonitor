// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorBase.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The validator base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using StatsWebRole.Exceptions;
    using StatsWebRole.Parameters;

    /// <summary>
    /// The validator base.
    /// </summary>
    public abstract class ValidatorBase
    {
        #region Fields

        /// <summary>
        /// The validation rule table.
        /// </summary>
        internal Dictionary<string, RuleParameterAssociation> ValidationRuleTable =
            new Dictionary<string, RuleParameterAssociation>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="parameterCollection">
        /// The parameter collection.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<ErrorObject> Validate(ParameterCollection parameterCollection)
        {
            var results = new List<ValidationResult>();
            var errorobjects = new List<ErrorObject>();

            foreach (var parameter in parameterCollection)
            {
                results.AddRange(
                    this.ValidationRuleTable[parameter.Key].Rule.Validate(
                        parameter.Value, 
                        Thread.CurrentThread.CurrentCulture));
            }

            return errorobjects;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The add rule.
        /// </summary>
        /// <param name="rulekey">
        /// The rulekey.
        /// </param>
        /// <param name="ruleAssociation">
        /// The rule association.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        internal bool AddRule(string rulekey, RuleParameterAssociation ruleAssociation)
        {
            try
            {
                this.ValidationRuleTable.Add(rulekey, ruleAssociation);
                return true;
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to add new rule", exception);
            }
        }

        #endregion
    }
}