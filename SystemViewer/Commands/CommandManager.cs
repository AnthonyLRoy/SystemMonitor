// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandManager.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The command manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SystemViewer.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;

    /// <summary>
    /// The command manager.
    /// </summary>
    public class CommandManager : ICommandManager
    {
        #region Fields

        /// <summary>
        /// The _types.
        /// </summary>
        private List<Type> _types;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManager"/> class.
        /// </summary>
        public CommandManager()
        {
            this.RegisterDomainCommands();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The select command.
        /// </summary>
        /// <param name="commandName">
        /// The command name.
        /// </param>
        /// <returns>
        /// The <see cref="ICommand"/>.
        /// </returns>
        public ICommand SelectCommand(string commandName)
        {
            try
            {
                return (ICommand)Activator.CreateInstance(this._types.Single(x => x.Name == commandName));
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The register domain commands.
        /// </summary>
        private void RegisterDomainCommands()
        {
            var type = typeof(ICommand);
            this._types =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => type.IsAssignableFrom(p) && !p.IsInterface)
                    .ToList();
        }

        #endregion
    }
}