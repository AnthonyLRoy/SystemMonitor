// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateCommand.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The delegate command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Infrastructure.Commands
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// The delegate command.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class DelegateCommand<T> : ICommand
    {
        #region Fields

        /// <summary>
        /// The _can execute.
        /// </summary>
        private readonly Predicate<object> _canExecute;

        /// <summary>
        /// The _execute.
        /// </summary>
        private readonly Action<T> _execute;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">
        /// The execute.
        /// </param>
        public DelegateCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">
        /// The execute.
        /// </param>
        /// <param name="canExecute">
        /// The can execute.
        /// </param>
        public DelegateCommand(Action<T> execute, Predicate<object> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

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
        public virtual bool CanExecute(object parameter)
        {
            return this._canExecute == null || this._canExecute(parameter);
        }

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public virtual void Execute(object parameter)
        {
            this._execute((T)parameter);
        }

        /// <summary>
        /// The raise can execute changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}