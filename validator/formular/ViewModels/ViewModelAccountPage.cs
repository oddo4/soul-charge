using formular.Classes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formular.ViewModels
{
    class ViewModelAccountPage : ViewModelBase
    {
        private string firstname;

        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; RaisePropertyChanged("Firstname"); }
        }
        private string surname;

        public string Surname
        {
            get { return surname; }
            set { surname = value; RaisePropertyChanged("Surname"); }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; RaisePropertyChanged("Email"); }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; RaisePropertyChanged("PhoneNumber"); }
        }

        //orders
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

        private List<Order> listAllOrders;

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

        private bool showHidden;

        public bool ShowHidden
        {
            get { return showHidden; }
            set
            {
                showHidden = value;
                ShowHiddenOrders();
                RaisePropertyChanged("ShowHidden");
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

        private void ShowHiddenOrders()
        {
            var list = ResultData;
            ResultData = listAllOrders;
            listAllOrders = list;
        }

        private async void ShowDataInListView(bool hidden = false)
        {
            API api = new API();

            var result = await api.GetData("Order", "id=" + App.User.ID);
            var c = await api.ParseJsonTask<Order>(result);

            listAllOrders = c;

            var noHidden = new List<Order>();

            foreach (Order order in c)
            {
                if (order.Hidden == 0)
                    noHidden.Add(order);
            }

            ResultData = noHidden;
        }
        private async void HideData()
        {
            API api = new API();
            var keyValues = CreateKeyValues(SelectedItem);

            var result = await api.GetPostData(keyValues, "Order");

            ResultData.Remove(SelectedItem);

            //ShowDataInListView();
        }

        public List<KeyValuePair<string, string>> CreateKeyValues(Order order)
        {
            List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

            keyValues.Add(new KeyValuePair<string, string>("Method", "Update"));

            keyValues.Add(new KeyValuePair<string, string>("Option", "Hide"));
            keyValues.Add(new KeyValuePair<string, string>("Order_ID", order.ID.ToString()));

            return keyValues;
        }

        private void NavigateBack()
        {
            NavigationServiceSingleton.GetNavigationService().NavigateBack();
        }
    }
}
