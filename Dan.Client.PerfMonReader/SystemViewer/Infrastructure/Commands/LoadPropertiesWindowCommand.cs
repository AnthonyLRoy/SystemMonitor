// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoadPropertiesWindowCommand.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The load properties window command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Infrastructure.Commands
{
    using System;
    using System.Windows.Input;

    using SystemViewer.Forms;

    /// <summary>
    /// The load properties window command.
    /// </summary>
    public class LoadPropertiesWindowCommand : ICommand
    {
        #region Constants

        /// <summary>
        /// The object properties container.
        /// </summary>
        private const string ObjectPropertiesContainer = "ObjectProperties";

        #endregion

        #region Public Events

        /// <summary>
        /// The can execute changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The can execute.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public void Execute(object parameter)
        {
            var win = (MainWindow)parameter;

            win.Layoutmanager.AddWindowToRight(PropertiesController.Pgrid, "Properties", ObjectPropertiesContainer);
        }

        #endregion
    }
}