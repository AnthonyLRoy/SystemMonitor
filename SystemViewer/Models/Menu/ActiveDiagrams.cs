// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActiveDiagrams.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The active diagrams.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Models.Menu
{
    using System.Collections.Generic;

    /// <summary>
    /// The active diagrams.
    /// </summary>
    public static class ActiveDiagrams
    {
        #region Static Fields

        /// <summary>
        /// The _diagrams.
        /// </summary>
        private static List<string> _diagrams;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the diagrams.
        /// </summary>
        public static List<string> Diagrams
        {
            get
            {
                return _diagrams;
            }

            set
            {
                _diagrams = value;
            }
        }

        #endregion
    }
}