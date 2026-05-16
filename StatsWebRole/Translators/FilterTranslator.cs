// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterTranslator.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The filter translator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Translators
{
    using StatsWebRole.Translators.TranslationMethod;

    /// <summary>
    /// The filter translator.
    /// </summary>
    public class FilterTranslator : BaseTranslator, ITranslator
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterTranslator"/> class.
        /// </summary>
        public FilterTranslator()
        {
            this.BuildAssociations();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether is translated.
        /// </summary>
        public bool IsTranslated
        {
            get
            {
                return this.Translated;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The build associations.
        /// </summary>
        private void BuildAssociations()
        {
            this.TranslationRuleAssociations.Add("filter", new NoTranslationTranslator());
        }

        #endregion
    }
}