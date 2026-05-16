// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertiesController.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The properties controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer
{
    using Xceed.Wpf.Toolkit.PropertyGrid;

    /// <summary>
    /// The properties controller.
    /// </summary>
    internal static class PropertiesController
    {
        #region Static Fields

        /// <summary>
        /// The _grid.
        /// </summary>
        private static PropertyGrid _grid = new PropertyGrid();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the pgrid.
        /// </summary>
        public static PropertyGrid Pgrid
        {
            get
            {
                return _grid;
            }

            set
            {
                _grid = value;
            }
        }

        #endregion
    }
}