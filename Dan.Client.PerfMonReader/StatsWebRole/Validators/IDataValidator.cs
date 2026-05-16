// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataValidator.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The DataValidator interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Validators
{
    using System.Collections.Generic;

    using StatsWebRole.Exceptions;
    using StatsWebRole.Parameters;

    /// <summary>
    /// The DataValidator interface.
    /// </summary>
    public interface IDataValidator
    {
        #region Public Methods and Operators

        /// <summary>
        /// The build associations.
        /// </summary>
        void BuildAssociations();

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="parameterCollection">
        /// The parameter collection.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<ErrorObject> Validate(ParameterCollection parameterCollection);

        #endregion
    }
}