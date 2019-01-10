using formular.Classes;
using formular.Pages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formular.ViewModels
{
    class ViewModelMainPage : ViewModelBase
    {
        private string firstname = App.User.Firstname;

        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; RaisePropertyChanged("Firstname"); }
        }

        private RelayCommand<object> showDataCommand;

        public RelayCommand<object> ShowDataCommand
        {
            get
            {
                return showDataCommand;
            }
            set
            {
                showDataCommand = value;
                RaisePropertyChanged("ShowDataCommand");
            }
        }
        private RelayCommand<object> addDataCommand;

        public RelayCommand<object> AddDataCommand
        {
            get
            {
                return addDataCommand;
            }
            set
            {
                addDataCommand = value;
                RaisePropertyChanged("AddDataCommand");
            }
        }
        private RelayCommand<object> deleteDataCommand;

        public RelayCommand<object> DeleteDataCommand
        {
            get
            {
                return deleteDataCommand;
            }
            set
            {
                deleteDataCommand = value;
                RaisePropertyChanged("DeleteDataCommand");
            }
        }

        public ViewModelMainPage()
        {
            ShowDataCommand = new RelayCommand<object>(Navigate, true);
            AddDataCommand = new RelayCommand<object>(Navigate, true);
            DeleteDataCommand = new RelayCommand<object>(Navigate, true);
        }

        private void Navigate(object type)
        {
            switch (int.Parse((string)type))
            {
                case 0:
                    NavigationServiceSingleton.GetNavigationService().NavigateToPage(new ShowItemsPage());
                    break;
                case 1:
                    //NavigationServiceSingleton.GetNavigationService().NavigateToPage(new FormPage());
                    break;
                case 2:
                    NavigationServiceSingleton.GetNavigationService().NavigateToPage(new AccountPage());
                    break;
                default:
                    break;
            }
                
        }
    }
}
