// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILibrary.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The Library interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Config
{
    /// <summary>
    /// The Library interface.
    /// </summary>
    public interface ILibrary
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