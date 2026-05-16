// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITranslationRuleAssociation.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The Translat interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Translators
{
    using StatsWebRole.Translators.TranslationMethod;

    /// <summary>
    /// The Translat interface.
    /// </summary>
    public interface ITranslat
    {
    }

    /// <summary>
    /// The TranslationRuleAssociation interface.
    /// </summary>
    public interface ITranslationRuleAssociation : ITranslat
    {
        #region Public Properties

        /// <summary>
        /// Gets the parameter name.
        /// </summary>
        string ParameterName { get; }

        /// <summary>
        /// Gets the translator.
        /// </summary>
        IValueTranslator Translator { get; }

        #endregion
    }
}