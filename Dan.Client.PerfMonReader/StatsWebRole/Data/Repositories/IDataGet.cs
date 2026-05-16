// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataGet.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The DataGet interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Data.Repositories
{
    using System.Collections.Generic;

    /// <summary>
    /// The DataGet interface.
    /// </summary>
    /// <typeparam name="TOut">
    /// </typeparam>
    public interface IDataGet<TOut>
    {
        #region Public Methods and Operators

        /// <summary>
        /// The execute.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<TOut> Execute();

        #endregion
    }
}