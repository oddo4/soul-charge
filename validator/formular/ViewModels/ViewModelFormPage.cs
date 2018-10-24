using FluentValidation.Results;
using formular.Classes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace formular.ViewModels
{
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
                Person.Firstname = FirstName;
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
                Person.Surname = SurName;
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

        private DateTime? date;

        public DateTime? Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                Person.DateOfBirth = Date;
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
                Person.IDNumber = IDNumber;
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

        public ViewModelFormPage()
        {
            SendCommand = new RelayCommand(ValidateForm, true);
        }

        private void ValidateForm()
        {
            ErrorMessage = "";

            ValidationResult results = validator.Validate(Person);

            if (results.IsValid)
            {
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

    }
}
