using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;


namespace SystemViewer.Commands
{
    public class CommandManager : ICommandManager
    {
        private List<Type> _types;

        public CommandManager()
        {
            RegisterDomainCommands();
        }

        private void RegisterDomainCommands()
        {
            var type = typeof (ICommand);
            _types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface).ToList<Type>();
        }

        public ICommand SelectCommand(string commandName)
        {
            try
            {
                return (ICommand) Activator.CreateInstance(_types.Single(x => x.Name == commandName));
            }
            catch
            {
                return null;
            }
        }
    }
}

