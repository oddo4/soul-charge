using FluentValidation.Results;
using formular.Classes;
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
        public Person Person = new Person();
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
        private string firstName;

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                Person.Firstname = firstName;
                BoolFirstName = !ValidationExtensions.Validate(validator, Person, "Firstname").IsValid;
                RaisePropertyChanged("FirstName");
            }
        }
        private bool boolFirstName = false;

        public bool BoolFirstName
        {
            get
            {
                return boolFirstName;
            }
            set
            {
                boolFirstName = value;
                RaisePropertyChanged("BoolFirstName");
            }
        }
        private string surName;

        public string SurName
        {
            get
            {
                return surName;
            }
            set
            {
                surName = value;
                Person.Surname = surName;
                BoolSurName = !ValidationExtensions.Validate(validator, Person, "Surname").IsValid;
                RaisePropertyChanged("SurName");
            }
        }
        private bool boolSurName = false;

        public bool BoolSurName
        {
            get
            {
                return boolSurName;
            }
            set
            {
                boolSurName = value;
                RaisePropertyChanged("BoolSurName");
            }
        }

        private DateTime date;

        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                Person.DateOfBirth = date;
                BoolDate = !ValidationExtensions.Validate(validator, Person, "Date").IsValid;
                RaisePropertyChanged("Date");
            }
        }
        private bool boolDate = false;

        public bool BoolDate
        {
            get
            {
                return boolDate;
            }
            set
            {
                boolDate = value;
                RaisePropertyChanged("BoolDate");
            }
        }
        private string idNumber;

        public string IDNumber
        {
            get
            {
                return idNumber;
            }
            set
            {
                idNumber = value;
                Person.IDNumber = idNumber;
                BoolIDNumber = !ValidationExtensions.Validate(validator, Person, "IDNumber").IsValid;
                RaisePropertyChanged("IDNumber");
            }
        }
        private bool boolIDNumber = false;

        public bool BoolIDNumber
        {
            get
            {
                return boolIDNumber;
            }
            set
            {
                boolIDNumber = value;
                RaisePropertyChanged("BoolIDNumber");
            }
        }

        private Gender gender = Gender.Undefined;
        public Gender Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
                Person.Gender = gender;
                RaisePropertyChanged("Gender");
            }
        }

        private RelayCommand<object> selectGenderCommand;

        public RelayCommand<object> SelectGenderCommand
        {
            get
            {
                return selectGenderCommand;
            }
            set
            {
                selectGenderCommand = value;
                RaisePropertyChanged("SelectGenderCommand");
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
            SendCommand = new RelayCommand(ValidateForm, true);
            GoBackCommand = new RelayCommand(NavigateBack, true);
            SelectGenderCommand = new RelayCommand<object>(SetGender, true);
        }

        public void ValidateForm()
        {
            ErrorMessage = "";
      
            ValidationResult results = validator.Validate(Person);

            if (results.IsValid)
            {
                AddData();

                ErrorMessage = "Odesláno.";
                SendNotice = Visibility.Visible;

            }
            else
            {
                foreach (ValidationFailure error in results.Errors)
                    ErrorMessage += error.ErrorMessage + " ";
                SendNotice = Visibility.Visible;
            }
        }

        public void AddData()
        {
            API api = new API();
            api.InsertData(Person);
        }

        public void NavigateBack()
        {
            NavigationServiceSingleton.GetNavigationService().NavigateBack();
        }

        public void SetGender(object parameter)
        {
            if (parameter != null)
            {
                switch (int.Parse((string)parameter))
                {
                    case 0:
                        Gender = Gender.Male;
                        break;
                    case 1:
                        Gender = Gender.Female;
                        break;
                    case 2:
                        Gender = Gender.Other;
                        break;
                    default:
                        Gender = Gender.Undefined;
                        break;

                }
            }
        }
    }
}
