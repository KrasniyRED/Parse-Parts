using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Parse_Parts.Infrastructure.Interfaces
{
    interface ICommandAsync : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}
