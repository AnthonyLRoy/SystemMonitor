// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TranslationRuleAssociation.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The translation rule association.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Translators
{
    using StatsWebRole.Translators.TranslationMethod;

    /// <summary>
    /// The translation rule association.
    /// </summary>
    public class TranslationRuleAssociation : ITranslationRuleAssociation
    {
        #region Fields

        /// <summary>
        /// The _parameter name.
        /// </summary>
        private readonly string _parameterName;

        /// <summary>
        /// The _translator.
        /// </summary>
        private readonly IValueTranslator _translator;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationRuleAssociation"/> class.
        /// </summary>
        /// <param name="parameterName">
        /// The parameter name.
        /// </param>
        /// <param name="translator">
        /// The translator.
        /// </param>
        public TranslationRuleAssociation(string parameterName, IValueTranslator translator)
        {
            this._parameterName = parameterName;
            this._translator = translator;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the parameter name.
        /// </summary>
        public string ParameterName
        {
            get
            {
                return this._parameterName;
            }
        }

        /// <summary>
        /// Gets the translator.
        /// </summary>
        public IValueTranslator Translator
        {
            get
            {
                return this._translator;
            }
        }

        #endregion
    }
}