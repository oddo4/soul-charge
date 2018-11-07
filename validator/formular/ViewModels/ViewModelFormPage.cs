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

    public class GenderConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int p = 4;
            int.TryParse((string)parameter, out p);
            if (value is Gender)
            {
                Gender v = (Gender)value;
                if (v == Gender.Male && p == 0)
                    return true;
                if (v == Gender.Female && p == 1)
                    return true;
                if (v == Gender.Other && p == 2)
                    return true;
            }
            return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int p = 4;
            int.TryParse((string)parameter, out p);
            if (value is bool && (bool)value)
                switch (p)
                {
                    case 0:
                        return Gender.Male;
                    case 1:
                        return Gender.Female;
                    case 2:
                        return Gender.Other;
                    default:
                        return Gender.Undefined;
                }
            return null;
        }
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

        private Gender gender = Gender.Undefined;
        public Gender Gender
        {
            get
            {
                return gender;
            }
            set
            {
                if (gender == value)
                    return;
                gender = value;
                RaisePropertyChanged("Gender");
            }
        }

        private RelayCommand<int> selectGenderCommand;

        public RelayCommand<int> SelectGenderCommand
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
            SelectGenderCommand = new RelayCommand<int>(SetGender, true);
        }

        private void ValidateForm()
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

        private void AddData()
        {
            API api = new API();
            api.InsertData(Person);
        }

        private void NavigateBack()
        {
            NavigationServiceSingleton.GetNavigationService().NavigateBack();
        }

        private void SetGender(int type)
        {
        }

    }
}
