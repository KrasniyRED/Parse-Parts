using CommunityToolkit.Mvvm.Input;
using Parse_Parts.Infrastructure.Commands;
using Parse_Parts.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Parse_Parts.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region SearchField
        
        private string _SearchField;
        public string SearchField
        {
            get => _SearchField;
            set => Set(ref _SearchField,value);
        }

        #endregion

        #region Close Command

        public ICommand CloseAppCommand {  get; }
        
        private void onCloseAppCommandExecuted(object obj)
        {
            Application.Current.Shutdown();
        }

        private bool canCloseAppCommandExecute(object obj)
        {
            return true;
        }

        #endregion

        #region OemSearch

        

        #endregion
        
        public MainWindowViewModel()
        {
            #region  Commands

            CloseAppCommand = new MainCommand(onCloseAppCommandExecuted, canCloseAppCommandExecute);

            #endregion
        }
    }
}
