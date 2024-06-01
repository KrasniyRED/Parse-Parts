using CommunityToolkit.Mvvm.Input;
using Parse_Parts.Infrastructure.Commands;
using Parse_Parts.Infrastructure.Interfaces;
using Parse_Parts.Models;
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
        IEnumerable<Advert> adverts;

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

        public ICommandAsync OemSearchCommand { get; }

        private async Task onOemSearchCommandExecuted()
        {
            var hub = ImportHub.getInstance();
            adverts = await hub.getAdverts(_SearchField);
        }

        private bool canOemSearchCommandEcecute(object obj)
        {
            return true;
        }

        #endregion
        
        public MainWindowViewModel()
        {
            #region  Commands

            CloseAppCommand = new MainCommand(onCloseAppCommandExecuted, canCloseAppCommandExecute);
            OemSearchCommand = new MainCommandAsync(onOemSearchCommandExecuted, canOemSearchCommandEcecute);

            #endregion
        }
    }
}
