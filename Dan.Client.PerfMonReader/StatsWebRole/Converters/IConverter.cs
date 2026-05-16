// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConverter.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The Converter interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Converters
{
    using System.Collections.Generic;

    /// <summary>
    /// The Converter interface.
    /// </summary>
    /// <typeparam name="Tin">
    /// </typeparam>
    /// <typeparam name="Tout">
    /// </typeparam>
    public interface IConverter<Tin, Tout>
    {
        #region Public Methods and Operators

        /// <summary>
        /// The convert class.
        /// </summary>
        /// <param name="eventusage">
        /// The eventusage.
        /// </param>
        /// <returns>
        /// The <see cref="Tout"/>.
        /// </returns>
        Tout ConvertClass(Tin eventusage);

        /// <summary>
        /// The convert enumerable.
        /// </summary>
        /// <param name="eventUsageList">
        /// The event usage list.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<Tout> ConvertEnumerable(IEnumerable<Tin> eventUsageList);

        #endregion
    }
}