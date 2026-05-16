// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReaderLibrarys.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The reader librarys.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Helpers.Libraries
{
    using System;
    using System.Reflection;

    using SystemViewer.Config;

    using Castle.Core.Internal;

    /// <summary>
    /// The reader librarys.
    /// </summary>
    public class ReaderLibrarys
    {
        #region Fields

        /// <summary>
        /// The metric reader library.
        /// </summary>
        private IMetricReaderLibrary metricReaderLibrary;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderLibrarys"/> class.
        /// </summary>
        /// <param name="metricReaderLibrary">
        /// The metric reader library.
        /// </param>
        public ReaderLibrarys(IMetricReaderLibrary metricReaderLibrary)
        {
            this.metricReaderLibrary = metricReaderLibrary;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The load libraries.
        /// </summary>
        internal void LoadLibraries()
        {
            foreach (var reader in this.metricReaderLibrary.readers)
            {
                Assembly.LoadFile(
                    string.Concat(
                        reader.location.IsNullOrEmpty() ? AppDomain.CurrentDomain.BaseDirectory : reader.location, 
                        @"\", 
                        reader.name));
            }
        }

        #endregion
    }
}