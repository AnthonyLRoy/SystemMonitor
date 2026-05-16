// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseTranslator.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The base translator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Translators
{
    using System.Collections.Generic;

    using StatsWebRole.Exceptions;
    using StatsWebRole.Parameters;
    using StatsWebRole.Translators.TranslationMethod;

    /// <summary>
    /// The base translator.
    /// </summary>
    public class BaseTranslator
    {
        #region Fields

        /// <summary>
        /// The translation rule associations.
        /// </summary>
        internal Dictionary<string, IValueTranslator> TranslationRuleAssociations =
            new Dictionary<string, IValueTranslator>();

        /// <summary>
        /// The results.
        /// </summary>
        protected List<ErrorObject> Results = new List<ErrorObject>();

        /// <summary>
        /// The translated.
        /// </summary>
        protected bool Translated;

        /// <summary>
        /// The validated.
        /// </summary>
        protected bool Validated;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the parametercollection.
        /// </summary>
        protected ParameterCollection Parametercollection { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The translate.
        /// </summary>
        /// <param name="parameterCollection">
        /// The parameter collection.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<ErrorObject> Translate(ParameterCollection parameterCollection)
        {
            var errorobjects = new List<ErrorObject>();

            foreach (var keyValuePair in parameterCollection)
            {
                // Find out if a rule Exists for this Parameter
                if (this.TranslationRuleAssociations.ContainsKey(keyValuePair.Key))
                {
                    List<ErrorObject> translationResults =
                        this.TranslationRuleAssociations[keyValuePair.Key].Translate(
                            keyValuePair.Key, 
                            parameterCollection);
                    if (translationResults != null)
                    {
                        errorobjects.AddRange(translationResults);
                    }
                }
            }

            this.Validated = errorobjects.Count == 0;
            return errorobjects;
        }

        #endregion
    }
}