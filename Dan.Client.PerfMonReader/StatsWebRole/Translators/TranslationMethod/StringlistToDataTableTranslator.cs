// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringlistToDataTableTranslator.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The stringlist to data table translator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace StatsWebRole.Translators.TranslationMethod
{
    using System.Collections.Generic;
    using System.Data;

    using StatsWebRole.Exceptions;
    using StatsWebRole.Parameters;

    /// <summary>
    /// The stringlist to data table translator.
    /// </summary>
    public class StringlistToDataTableTranslator : IValueTranslator
    {
        #region Public Methods and Operators

        /// <summary>
        /// The translate.
        /// </summary>
        /// <param name="parametername">
        /// The parametername.
        /// </param>
        /// <param name="parameterCollection">
        /// The parameter collection.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<ErrorObject> Translate(string parametername, ParameterCollection parameterCollection)
        {
            var parameter = parameterCollection[parametername];

            var dataTable = new DataTable();
            dataTable.Columns.Add("PerformanceID", typeof(string));
            foreach (var stringdata in (List<string>)parameter.Value)
            {
                DataRow newDataRow = dataTable.NewRow();
                newDataRow["PerformanceID"] = stringdata;
                dataTable.Rows.Add(newDataRow);
            }

            parameter.TranslatedValue = dataTable;
            return null;
        }

        #endregion
    }
}