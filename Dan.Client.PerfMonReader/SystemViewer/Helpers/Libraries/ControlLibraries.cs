// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlLibraries.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The control libraries.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Helpers.Libraries
{
    using System;
    using System.Reflection;

    using SystemViewer.Config;

    using Castle.Core.Internal;

    /// <summary>
    /// The control libraries.
    /// </summary>
    public class ControlLibraries
    {
        #region Fields

        /// <summary>
        /// The external reader library.
        /// </summary>
        private readonly IExternalReaderLibrary externalReaderLibrary;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlLibraries"/> class.
        /// </summary>
        public ControlLibraries()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlLibraries"/> class.
        /// </summary>
        /// <param name="externalConfig">
        /// The external config.
        /// </param>
        public ControlLibraries(IExternalReaderLibrary externalConfig)
        {
            // TODO: Complete member initialization
            this.externalReaderLibrary = externalConfig;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The load libraries.
        /// </summary>
        public void LoadLibraries()
        {
            foreach (var library in this.externalReaderLibrary.libraries)
            {
                Assembly.LoadFile(
                    string.Concat(
                        library.location.IsNullOrEmpty() ? AppDomain.CurrentDomain.BaseDirectory : library.location, 
                        @"\", 
                        library.name));
            }
        }

        /// <summary>
        /// The prepare libraries.
        /// </summary>
        public void PrepareLibraries()
        {
        }

        #endregion
    }
}