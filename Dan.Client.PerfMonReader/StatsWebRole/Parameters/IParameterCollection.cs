// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IParameterCollection.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The ParameterCollection interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Parameters
{
    using System.Collections.Generic;

    using StatsWebRole.Exceptions;

    /// <summary>
    /// The ParameterCollection interface.
    /// </summary>
    public interface IParameterCollection
    {
        #region Public Methods and Operators

        /// <summary>
        /// The translate.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<ErrorObject> Translate();

        /// <summary>
        /// The validate.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<ErrorObject> Validate();

        #endregion
    }
}