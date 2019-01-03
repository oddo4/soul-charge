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


        private Order selectedItem;

        public Order SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;             
                RaisePropertyChanged("SelectedItem");
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

        private async void ShowDataInListView(bool hidden = false)
        {
            API api = new API();
            var result = await api.GetAllJsonTask("Order");
            var c = await api.ParseJsonTask<Order>(result);

            ResultData = c;
        }
        private async void HideData()
        {
            API api = new API();
            var keyValues = CreateKeyValues(SelectedItem);

            var result = await api.GetPostData(keyValues, "Order");

            //ShowDataInListView();
        }

        public List<KeyValuePair<string, string>> CreateKeyValues(Order order)
        {
            List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

            keyValues.Add(new KeyValuePair<string, string>("Method", "Update"));

            keyValues.Add(new KeyValuePair<string, string>("Option", "Hide"));
            keyValues.Add(new KeyValuePair<string, string>("Order_ID", order.ID.ToString()));
            keyValues.Add(new KeyValuePair<string, string>("Person_ID", order.Person_ID.ToString()));

            return keyValues;
        }

        private void NavigateBack()
        {
            NavigationServiceSingleton.GetNavigationService().NavigateBack();
        }
    }
}
