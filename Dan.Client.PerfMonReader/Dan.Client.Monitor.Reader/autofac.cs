// --------------------------------------------------------------------------------------------------------------------
// <copyright file="autofac.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The autofac.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dan.Client.Monitor.Reader
{
    using Autofac;

    /// <summary>
    /// The autofac.
    /// </summary>
    internal static class autofac
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        public static IContainer Container { get; set; }

        #endregion
    }
}