// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileMenuEventArgs.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The file menu handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Models.Menu
{
    using System;

    /// <summary>
    /// The file menu handler.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    public delegate void FileMenuHandler(object sender, FileMenuEventArgs args);

    /// <summary>
    /// The file menu event args.
    /// </summary>
    public class FileMenuEventArgs : EventArgs
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileMenuEventArgs"/> class.
        /// </summary>
        public FileMenuEventArgs()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileMenuEventArgs"/> class.
        /// </summary>
        /// <param name="commandName">
        /// The command name.
        /// </param>
        public FileMenuEventArgs(string commandName)
        {
            this.CommandName = this.CommandName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the command name.
        /// </summary>
        public string CommandName { get; set; }

        #endregion
    }
}