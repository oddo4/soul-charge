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
        public Person newPerson = App.User.Person;

        private string username = App.User.Username;

        public string Username
        {
            get { return username; }
            set { username = value; RaisePropertyChanged("Username"); }
        }

        private string firstname = App.User.Person.Firstname;

        public string Firstname
        {
            get { return firstname; }
            set
            {
                firstname = value;
                newPerson.Firstname = firstname;
                RaisePropertyChanged("Firstname");
            }
        }
        private string surname = App.User.Person.Surname;

        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                newPerson.Surname = surname;
                RaisePropertyChanged("Surname");
            }
        }
        private string email = App.User.Person.Email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                newPerson.Email = email;
                RaisePropertyChanged("Email");
            }
        }

        private string phoneNumber = App.User.Person.PhoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                newPerson.PhoneNumber = phoneNumber;
                RaisePropertyChanged("PhoneNumber");
            }
        }
        private DateTime? dateOfBirth = App.User.Person.Date;
        public DateTime? DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                dateOfBirth = value;
                if (dateOfBirth != null)
                    newPerson.DateOfBirth = ((DateTime)dateOfBirth).ToString("yyyy-MM-dd");
                RaisePropertyChanged("DateOfBirth");
            }
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

        private bool hideBtnEnabled = false;

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
        private bool showHiddenEnabled = false;

        public bool ShowHiddenEnabled
        {
            get
            {
                return showHiddenEnabled;
            }
            set
            {
                showHiddenEnabled = value;
                RaisePropertyChanged("ShowHiddenEnabled");
            }
        }

        private bool showHidden = false;

        public bool ShowHidden
        {
            get { return showHidden; }
            set
            {
                showHidden = value;
                ShowHiddenOrders();
                if (showHidden)
                {
                    HideBtnEnabled = false;
                }
                else
                {
                    HideBtnEnabled = true;
                }
                RaisePropertyChanged("ShowHidden");
            }
        }

        private bool saveInfoBtnEnabled = true;

        public bool SaveInfoBtnEnabled
        {
            get
            {
                return saveInfoBtnEnabled;
            }
            set
            {
                saveInfoBtnEnabled = value;
                RaisePropertyChanged("SaveInfoBtnEnabled");
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

        private RelayCommand sendCommand;

        public RelayCommand SendCommand
        {
            get
            {
                return sendCommand;
            }
            set
            {
                sendCommand = value;
                RaisePropertyChanged("SendCommand");
            }
        }

        public ViewModelAccountPage()
        {
            HideDataCommand = new RelayCommand(HideData, true);
            GoBackCommand = new RelayCommand(NavigateBack, true);
            SendCommand = new RelayCommand(SavePersonData, true);
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

            listAllOrders = c.OrderByDescending(x => x.Date).ToList();

            var noHidden = new List<Order>();

            foreach (Order order in c)
            {
                if (order.Hidden == 0)
                    noHidden.Add(order);
            }

            ResultData = noHidden.OrderByDescending(x => x.Date).ToList();
            ShowHiddenEnabled = true;
            HideBtnEnabled = true;
        }
        private async void HideData()
        {
            var item = SelectedItem;
            var keyValues = CreateKeyValues(item);

            var result = await App.Api.GetPostData(keyValues, "Order");

            ResultData.Remove(item);

            //ShowDataInListView();
        }

        private async void SavePersonData()
        {
            SaveInfoBtnEnabled = false;
            var updatePersonResult = await App.Api.GetPostData(CreateKeyValues(null, true), "PersonData");

            var keyValues = new List<KeyValuePair<string, string>>();

            keyValues.Add(new KeyValuePair<string, string>("Method", "Get"));

            keyValues.Add(new KeyValuePair<string, string>("Option", "PersonData"));
            keyValues.Add(new KeyValuePair<string, string>("Person_ID", App.User.ID.ToString()));

            var getPersonResult = await App.Api.GetPostData(keyValues, "PersonData");
            var parseResult = await App.Api.ParseJsonTask<Person>(getPersonResult);

            App.User.Person = parseResult.FirstOrDefault();

            SaveInfoBtnEnabled = true;
        }

        public List<KeyValuePair<string, string>> CreateKeyValues(Order order, bool person = false)
        {
            List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

            if (person)
            {
                keyValues.Add(new KeyValuePair<string, string>("Method", "Update"));

                keyValues.Add(new KeyValuePair<string, string>("Option", "UpdatePersonData"));
                keyValues.Add(new KeyValuePair<string, string>("Firstname", newPerson.Firstname));
                keyValues.Add(new KeyValuePair<string, string>("Surname", newPerson.Surname));
                keyValues.Add(new KeyValuePair<string, string>("Email", newPerson.Email));
                keyValues.Add(new KeyValuePair<string, string>("PhoneNumber", newPerson.PhoneNumber));
                keyValues.Add(new KeyValuePair<string, string>("DateOfBirth", newPerson.DateOfBirth));
                keyValues.Add(new KeyValuePair<string, string>("Person_ID", App.User.ID.ToString()));
            }
            else
            {
                keyValues.Add(new KeyValuePair<string, string>("Method", "Update"));

                keyValues.Add(new KeyValuePair<string, string>("Option", "Hide"));
                keyValues.Add(new KeyValuePair<string, string>("Hide", (order.Hidden == 0) ? "1" : "0"));
                keyValues.Add(new KeyValuePair<string, string>("Order_ID", order.ID.ToString()));
            }
           

            return keyValues;
        }

        private void NavigateBack()
        {
            NavigationServiceSingleton.GetNavigationService().NavigateBack();
        }
    }
}
