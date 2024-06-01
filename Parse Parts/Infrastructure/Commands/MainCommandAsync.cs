using Parse_Parts.Infrastructure.Commands.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse_Parts.Infrastructure.Commands
{
    internal class MainCommandAsync : CommandAsync
    {
        private readonly Func<Task> _Execute;
        private readonly Func<object,bool> _CanExecute;

        public MainCommandAsync(Func<Task> Execute, Func<object,bool> CanExecute)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute;
        }
        public override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;
        public override Task ExecuteAsync(object parameter)
        {
            return _Execute();
        }
    }
}
