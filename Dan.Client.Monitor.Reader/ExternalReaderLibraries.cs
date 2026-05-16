// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExternalReaderLibraries.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The external reader library section.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dan.Client.Monitor.Reader
{
    using System.Collections.Generic;

    using SimpleConfigSections;

    /// <summary>
    /// The external reader library section.
    /// </summary>
    public class ExternalReaderLibrarySection : ConfigurationSection<IExternalReaderLibrary>
    {
    }

    /// <summary>
    /// The ExternalReaderLibrary interface.
    /// </summary>
    public interface IExternalReaderLibrary
    {
        #region Public Properties

        /// <summary>
        /// Gets the string value.
        /// </summary>
        string StringValue { get; }

        /// <summary>
        /// Gets the libraries.
        /// </summary>
        IEnumerable<ILibrary> libraries { get; }

        /// <summary>
        /// Gets the library.
        /// </summary>
        ILibrary library { get; }

        #endregion
    }

    /// <summary>
    /// The Library interface.
    /// </summary>
    public interface ILibrary
    {
        #region Public Properties

        /// <summary>
        /// Gets the location.
        /// </summary>
        string location { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        string name { get; }

        #endregion
    }
}