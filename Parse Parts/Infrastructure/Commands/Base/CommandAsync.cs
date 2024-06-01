using Parse_Parts.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Parse_Parts.Infrastructure.Commands.Base
{
    internal abstract class CommandAsync : ICommandAsync
    {
        public abstract bool CanExecute(object parameter);
        
        public abstract Task ExecuteAsync(object parameter);
        
        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        protected void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
