using FluentValidation.Results;
using formular.Classes;
using formular.Pages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace formular.ViewModels
{
    public enum Gender
    {
        Male,
        Female,
        Other,
        Undefined
    }
    class ViewModelFormPage : ViewModelBase
    {
        public PersonValidator validator = new PersonValidator();

        private Visibility sendNotice = Visibility.Hidden;

        public Visibility SendNotice
        {
            get
            {
                return sendNotice;
            }
            set
            {
                sendNotice = value;
                RaisePropertyChanged("SendNotice");
            }
        }
        private string errorMessage = "";

        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
                RaisePropertyChanged("ErrorMessage");
            }
        }
        private string username;

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                RaisePropertyChanged("Username");
            }
        }
        private bool boolUsername = false;

        public bool BoolUsername
        {
            get
            {
                return boolUsername;
            }
            set
            {
                boolUsername = value;
                RaisePropertyChanged("BoolUsername");
            }
        }
        private string password;

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }
        private bool boolPassword = false;

        public bool BoolPassword
        {
            get
            {
                return boolPassword;
            }
            set
            {
                boolPassword = value;
                RaisePropertyChanged("BoolPassword");
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

        public ViewModelFormPage()
        {
            SendCommand = new RelayCommand(AddData, true);
            GoBackCommand = new RelayCommand(NavigateBack, true);
        }

        public void ValidateForm()
        {
            ErrorMessage = "";

            //ValidationResult results = validator.Validate(Person);

            //if (results.IsValid)
            //{
            //    AddData();

            //    ErrorMessage = "Odesláno.";
            //    SendNotice = Visibility.Visible;
            //}
            //else
            //{
            //    foreach (ValidationFailure error in results.Errors)
            //        ErrorMessage += error.ErrorMessage + " ";
            //    SendNotice = Visibility.Visible;
            //}
        }

        public async void AddData()
        {
            API api = new API();
            var loginResult = await api.GetPostData(CreateKeyValues(new Login() { Username = username, Password = password }), "Person");
            var loginData = await api.ParseJsonTask<Login>(loginResult);

            if (loginData.Count == 1)
            {
                var login = loginData.FirstOrDefault();
                var personResult = await api.GetPostData(CreateKeyValues(login, false), "PersonData");
                var personData = await api.ParseJsonTask<Person>(personResult);

                App.User = personData.FirstOrDefault();

                NavigationServiceSingleton.GetNavigationService().NavigateToPage(new MainPage());
            }

            //api.InsertPersonData(Person);
        }

        public List<KeyValuePair<string, string>> CreateKeyValues(Login loginData, bool login = true)
        {
            List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

            keyValues.Add(new KeyValuePair<string, string>("Method", "Get"));
            if (login)
            {
                keyValues.Add(new KeyValuePair<string, string>("Table", "Person"));
                keyValues.Add(new KeyValuePair<string, string>("Username", loginData.Username));
                keyValues.Add(new KeyValuePair<string, string>("Password", loginData.Password));
            }
            else
            {
                keyValues.Add(new KeyValuePair<string, string>("Table", "PersonData"));
                keyValues.Add(new KeyValuePair<string, string>("Person_ID", loginData.ID.ToString()));
            }

            return keyValues;
        }

        public void NavigateBack()
        {
            NavigationServiceSingleton.GetNavigationService().NavigateBack();
        }
    }
}
