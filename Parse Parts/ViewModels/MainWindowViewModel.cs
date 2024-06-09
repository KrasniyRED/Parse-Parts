using Microsoft.Data.Sqlite;
using Parse_Parts.Infrastructure.Commands;
using Parse_Parts.Infrastructure.Interfaces;
using Parse_Parts.Models;
using Parse_Parts.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Parse_Parts.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region DbConnection

        SqliteConnection dbConnection = new SqliteConnection("Data Source=partsCatalog.db;Mode=ReadOnly");

        #endregion

        #region AdvertsCollection

        private Collection<Advert> _Adverts;
        public Collection<Advert> Adverts
        {
            get => _Adverts;
            set => Set(ref _Adverts, value);
        }

        #endregion

        #region OemSearchField

        private string _SearchField;
        public string SearchField
        {
            get => _SearchField;
            set => Set(ref _SearchField,value);
        }

        #endregion

        #region MachineSearchFields

        #region Brands

        private List<string> _Brands;
        public List<string> Brands 
        {
            get => _Brands;
            set => Set(ref _Brands, value);
        }

        private int _SelectedBrand;
        public int SelectedBrand
        {
            get =>_SelectedBrand;
            set=> Set(ref _SelectedBrand, value);
        }

        #endregion

        #region CarModels

        private List<string> _CarModels;
        public List<string> CarModels
        {
            get => _CarModels;
            set => Set(ref _CarModels, value);
        }

        private int _SelectedCarModel;
        public int SelectedCarModel
        {
            get => _SelectedCarModel;
            set => Set(ref _SelectedCarModel, value);
        }

        #endregion

        #region PartsNames

        private List<string> _PartsNames;
        public List<string> PartsNames
        {
            get => _PartsNames;
            set => Set(ref _PartsNames, value);
        }

        private int _SelectedPartName;
        public int SelectedPartName
        {
            get => _SelectedPartName;
            set => Set(ref _SelectedPartName, value);
        }

        #endregion

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
            Adverts = await hub.getAdverts(_SearchField);
        }

        private bool canOemSearchCommandEcecute(object obj)
        {
            if(_SearchField == null || _SearchField == "") return false;
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
