using FluentValidation.Results;
using formular.Classes;
using formular.Pages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace formular.ViewModels
{
    class ViewModelRegisterPage : ViewModelBase
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
        //private string email;
        //public string Email
        //{
        //    get
        //    {
        //        return email;
        //    }
        //    set
        //    {
        //        email = value;
        //        newLogin.Username = username;
        //        RaisePropertyChanged("Email");
        //    }
        //}
        //private bool boolEmail = false;

        //public bool BoolEmail
        //{
        //    get
        //    {
        //        return boolEmail;
        //    }
        //    set
        //    {
        //        boolEmail = value;
        //        RaisePropertyChanged("BoolEmail");
        //    }
        //}
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

        private string passwordAgain;

        public string PasswordAgain
        {
            get
            {
                return passwordAgain;
            }
            set
            {
                passwordAgain = value;
                //newLogin.Password = password;
                RaisePropertyChanged("PasswordAgain");
            }
        }
        private bool boolPasswordAgain = false;

        public bool BoolPasswordAgain
        {
            get
            {
                return boolPasswordAgain;
            }
            set
            {
                boolPasswordAgain = value;
                RaisePropertyChanged("BoolPasswordAgain");
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

        private RelayCommand loginCommand;

        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand;
            }
            set
            {
                loginCommand = value;
                RaisePropertyChanged("LoginCommand");
            }
        }

        public ViewModelRegisterPage()
        {
            SendCommand = new RelayCommand(ValidateForm, true);
            LoginCommand = new RelayCommand(NavigateBack, true);
        }

        public void ValidateForm()
        {
            SendBtnEnabled = false;
            ErrorMessage = "";

            ValidationResult results = validator.Validate(newLogin);

            if (results.IsValid)
            {
                if (newLogin.Password == passwordAgain)
                    InsertData();
                else
                {
                    ErrorMessage += "Hesla se neshodují. ";
                    SendBtnEnabled = true;
                }
            }
            else
            {
                foreach (ValidationFailure error in results.Errors)
                    ErrorMessage += error.ErrorMessage + " ";
                SendNotice = Visibility.Visible;
                SendBtnEnabled = true;
            }
        }

        public async void InsertData()
        {
            var registerResult = await App.Api.InsertData(CreateKeyValues(newLogin), "Person");

            var keyValues = new List<KeyValuePair<string, string>>();

            keyValues.Add(new KeyValuePair<string, string>("Method", "Get"));
            keyValues.Add(new KeyValuePair<string, string>("Option", "Login"));
            keyValues.Add(new KeyValuePair<string, string>("Username", newLogin.Username));
            keyValues.Add(new KeyValuePair<string, string>("Password", SHA.GenerateSHA256String(newLogin.Password)));

            var loginResult = await App.Api.GetPostData(keyValues, "Person");
            var loginData = await App.Api.ParseJsonTask<Login>(loginResult);

            var login = loginData.FirstOrDefault();

            var insertPersonResult = await App.Api.InsertData(CreateKeyValues(login, true), "PersonData");

            NavigateBack();
        }

        public List<KeyValuePair<string, string>> CreateKeyValues(Login loginData, bool person = false)
        {
            List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

            keyValues.Add(new KeyValuePair<string, string>("Method", "Insert"));

            if (person)
            {
                keyValues.Add(new KeyValuePair<string, string>("Option", "NewPersonData"));
                keyValues.Add(new KeyValuePair<string, string>("Person_ID", loginData.ID.ToString()));
                keyValues.Add(new KeyValuePair<string, string>("Date", DateTime.Now.ToString("yyyy-MM-dd")));
            }
            else
            {
                keyValues.Add(new KeyValuePair<string, string>("Option", "Register"));
                keyValues.Add(new KeyValuePair<string, string>("Username", loginData.Username));
                keyValues.Add(new KeyValuePair<string, string>("Password", SHA.GenerateSHA256String(loginData.Password)));
            }



            return keyValues;
        }

        public void NavigateBack()
        {
            NavigationServiceSingleton.GetNavigationService().NavigateBack();
        }
    }
}