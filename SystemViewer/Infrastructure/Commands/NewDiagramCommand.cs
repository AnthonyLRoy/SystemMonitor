// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewDiagramCommand.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The new diagram command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Infrastructure.Commands
{
    using System;
    using System.Windows.Input;

    using SystemViewer.Forms;
    using SystemViewer.Forms.dialogs;

    /// <summary>
    /// The new diagram command.
    /// </summary>
    public class NewDiagramCommand : ICommand
    {
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
            var diagram = new frmNewDiagram();
            diagram.ShowDialog();
            if (diagram.DialogResult == true)
            {
                win.Layoutmanager.AddNewDiagram(diagram.DiagramName);
            }
        }

        #endregion
    }
}