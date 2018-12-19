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
    class ViewModelAccountPage : ViewModelBase
    {
        private List<Order> resultData;

        public List<Order> ResultData
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

        private bool hideBtnEnabled = true;

        public bool HideBtnEnabled
        {
            get
            {
                return hideBtnEnabled;
            }
            set
            {
                hideBtnEnabled = value;
                RaisePropertyChanged("HideBtnEnabled");
            }
        }

        private RelayCommand hideDataCommand;

        public RelayCommand HideDataCommand
        {
            get
            {
                return hideDataCommand;
            }
            set
            {
                hideDataCommand = value;
                RaisePropertyChanged("HideDataCommand");
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

        public ViewModelAccountPage()
        {
            HideDataCommand = new RelayCommand(HideData, true);
            GoBackCommand = new RelayCommand(NavigateBack, true);
            ShowDataInListView();
        }

        private async void ShowDataInListView()
        {
            API api = new API();
            var result = await api.GetAllJsonTask("Order");
            var c = await api.ParseJsonTask<Order>(result);

            ResultData = c;
        }
        private async void HideData()
        {
            
        }
        private void NavigateBack()
        {
            NavigationServiceSingleton.GetNavigationService().NavigateBack();
        }
    }
}
