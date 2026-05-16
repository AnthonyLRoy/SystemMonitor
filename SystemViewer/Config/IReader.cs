// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReader.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The Reader interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Config
{
    /// <summary>
    /// The Reader interface.
    /// </summary>
    public interface IReader
    {
        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether active.
        /// </summary>
        bool active { get; }

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