using formular.Classes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formular.ViewModels
{
    class ViewModelDeletePage : ViewModelBase
    {
        private List<Person> resultData;

        public List<Person> ResultData
        {
            get
            {
                return resultData;
            }
            set
            {
                resultData = value;
                RaisePropertyChanged("ResultData");
            }
        }

        private bool deleteBtnEnabled = true;

        public bool DeleteBtnEnabled
        {
            get
            {
                return deleteBtnEnabled;
            }
            set
            {
                deleteBtnEnabled = value;
                RaisePropertyChanged("DeleteBtnEnabled");
            }
        }

        private RelayCommand deleteDataCommand;

        public RelayCommand DeleteDataCommand
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


        private int itemIsSelected;

        public int ItemIsSelected
        {
            get
            {
                return itemIsSelected;
            }
            set
            {
                itemIsSelected = value;             
                RaisePropertyChanged("ItemIsSelected");
            }
        }

        private RelayCommand goBackCommand;

        public RelayCommand GoBackCommand
        {
            get
            {
                return goBackCommand;
            }
            set
            {
                goBackCommand = value;
                RaisePropertyChanged("GoBackCommand");
            }
        }

        public ViewModelDeletePage()
        {
            DeleteDataCommand = new RelayCommand(DeleteData, true);
            GoBackCommand = new RelayCommand(NavigateBack, true);
            ShowDataInListView();
        }

        private async void ShowDataInListView()
        {
            API api = new API();
            var result = await api.GetPostsJsonTask("");
            var c = await api.ParsePostJsonTask(result);

            ResultData = c;
        }
        private async void DeleteData()
        {
            API api = new API();
        }
        private void NavigateBack()
        {
            NavigationServiceSingleton.GetNavigationService().NavigateBack();
        }
    }
}
