// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IParameter.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The Parameter interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Parameters.Parameter
{
    /// <summary>
    /// The Parameter interface.
    /// </summary>
    public interface IParameter
    {
        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether is translated.
        /// </summary>
        bool IsTranslated { get; }

        /// <summary>
        /// Gets a value indicating whether is validated.
        /// </summary>
        bool IsValidated { get; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the translated value.
        /// </summary>
        object TranslatedValue { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        object Value { get; }

        #endregion
    }
}