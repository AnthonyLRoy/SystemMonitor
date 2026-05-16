// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepositoryDataModel.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The RepositoryDataModel interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Data.RepositoryModels
{
    using System.Data;

    /// <summary>
    /// The RepositoryDataModel interface.
    /// </summary>
    internal interface IRepositoryDataModel
    {
        #region Public Methods and Operators

        /// <summary>
        /// The transform reader to class.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        void TransformReaderToClass(IDataReader reader);

        #endregion
    }
}