// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceStatisticsListTranslator.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The service statistics list translator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Translators
{
    using StatsWebRole.Translators.TranslationMethod;

    /// <summary>
    /// The service statistics list translator.
    /// </summary>
    public class ServiceStatisticsListTranslator : BaseTranslator, ITranslator
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceStatisticsListTranslator"/> class.
        /// </summary>
        public ServiceStatisticsListTranslator()
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
            this.TranslationRuleAssociations.Add("requestList", new StringlistToDataTableTranslator());
        }

        #endregion
    }
}