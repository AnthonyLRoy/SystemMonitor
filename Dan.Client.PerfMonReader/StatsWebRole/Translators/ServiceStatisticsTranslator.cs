// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceStatisticsTranslator.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The service statistics translator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Translators
{
    using StatsWebRole.Translators.TranslationMethod;

    /// <summary>
    /// The service statistics translator.
    /// </summary>
    public class ServiceStatisticsTranslator : BaseTranslator, ITranslator
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceStatisticsTranslator"/> class.
        /// </summary>
        public ServiceStatisticsTranslator()
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

        #region Public Methods and Operators

        /// <summary>
        /// The build associations.
        /// </summary>
        public void BuildAssociations()
        {
            this.TranslationRuleAssociations.Add("groupName", new NoTranslationTranslator());
        }

        #endregion
    }
}