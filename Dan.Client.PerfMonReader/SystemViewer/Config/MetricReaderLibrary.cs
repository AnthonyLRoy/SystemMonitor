// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetricReaderLibrary.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The metric reader library section.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SystemViewer.Config
{
    using System.Collections.Generic;

    using SimpleConfigSections;

    /// <summary>
    /// The metric reader library section.
    /// </summary>
    public class MetricReaderLibrarySection : ConfigurationSection<IMetricReaderLibrary>
    {
    }

    /// <summary>
    /// The MetricReaderLibrary interface.
    /// </summary>
    public interface IMetricReaderLibrary
    {
        #region Public Properties

        /// <summary>
        /// Gets the string value.
        /// </summary>
        string StringValue { get; }

        /// <summary>
        /// Gets the library.
        /// </summary>
        IReader library { get; }

        /// <summary>
        /// Gets the readers.
        /// </summary>
        IEnumerable<IReader> readers { get; }

        #endregion
    }
}