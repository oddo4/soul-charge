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
    class ViewModelFormPage : ViewModelBase
    {
        public Login newLogin = new Login();

        public LoginValidator validator = new LoginValidator();

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
                newLogin.Username = username;
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
                newLogin.Password = password;
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

        private bool sendBtnEnabled = true;

        public bool SendBtnEnabled
        {
            get
            {
                return sendBtnEnabled;
            }
            set
            {
                sendBtnEnabled = value;
                RaisePropertyChanged("SendBtnEnabled");
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

        private RelayCommand registerCommand;

        public RelayCommand RegisterCommand
        {
            get
            {
                return registerCommand;
            }
            set
            {
                registerCommand = value;
                RaisePropertyChanged("RegisterCommand");
            }
        }

        public ViewModelFormPage()
        {
            SendCommand = new RelayCommand(ValidateForm, true);
            RegisterCommand = new RelayCommand(RegisterNavigate, true);
        }

        public void ValidateForm()
        {
            SendBtnEnabled = false;
            ErrorMessage = "";

            ValidationResult results = validator.Validate(newLogin);

            if (results.IsValid)
            {
                GetData();
            }
            else
            {
                foreach (ValidationFailure error in results.Errors)
                    ErrorMessage += error.ErrorMessage + " ";
                SendNotice = Visibility.Visible;
                SendBtnEnabled = true;
            }
        }

        public async void GetData()
        {
            var loginResult = await App.Api.GetPostData(CreateKeyValues(newLogin), "Person");
            var loginData = await App.Api.ParseJsonTask<Login>(loginResult);

            if (loginData.Count == 1)
            {
                var login = loginData.FirstOrDefault();
                var personResult = await App.Api.GetPostData(CreateKeyValues(login, false), "PersonData");
                var personData = await App.Api.ParseJsonTask<Person>(personResult);

                login.Person = personData.FirstOrDefault();
                App.User = login;

                NavigationServiceSingleton.GetNavigationService().NavigateToPage(new MainPage());

                ErrorMessage = "Přihlášen.";
                SendNotice = Visibility.Visible;
            }
            else
            {
                ErrorMessage = "Špatné přihlašovací údaje.";
                SendNotice = Visibility.Visible;
                SendBtnEnabled = true;
            }

        }

        public List<KeyValuePair<string, string>> CreateKeyValues(Login loginData, bool login = true)
        {
            List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

            keyValues.Add(new KeyValuePair<string, string>("Method", "Get"));
            if (login)
            {
                keyValues.Add(new KeyValuePair<string, string>("Option", "Login"));
                keyValues.Add(new KeyValuePair<string, string>("Username", loginData.Username));
                keyValues.Add(new KeyValuePair<string, string>("Password", SHA.GenerateSHA256String(loginData.Password)));
            }
            else
            {
                keyValues.Add(new KeyValuePair<string, string>("Option", "PersonData"));
                keyValues.Add(new KeyValuePair<string, string>("Person_ID", loginData.ID.ToString()));
            }

            return keyValues;
        }

        public void RegisterNavigate()
        {
            NavigationServiceSingleton.GetNavigationService().NavigateToPage(new RegisterPage());
        }
    }
}
