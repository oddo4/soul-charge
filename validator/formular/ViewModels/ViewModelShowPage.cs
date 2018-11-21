using FluentValidation.Results;
using formular.Classes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace formular.ViewModels
{
    class ViewModelShowItemsPage : ViewModelBase
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
        private List<Item> itemsData;

        public List<Item> ItemsData
        {
            get
            {
                return itemsData;
            }
            set
            {
                itemsData = value;
                RaisePropertyChanged("ItemsData");
            }
        }
        private List<Item> orderListData;

        public List<Item> OrderListData
        {
            get
            {
                return orderListData;
            }
            set
            {
                orderListData = value;
                RaisePropertyChanged("OrderListData");
            }
        }
        private Item selectedItemToAdd;

        public Item SelectedItemToAdd
        {
            get
            {
                return selectedItemToAdd;
            }
            set
            {
                selectedItemToAdd = value;
                RaisePropertyChanged("SelectedItemToAdd");
            }
        }
        private Item selectedItemToRemove;

        public Item SelectedItemToRemove
        {
            get
            {
                return selectedItemToRemove;
            }
            set
            {
                selectedItemToRemove = value;
                RaisePropertyChanged("SelectedItemToRemove");
            }
        }
        private RelayCommand<bool> moveItemCommand;

        public RelayCommand<bool> MoveItemCommand
        {
            get
            {
                return moveItemCommand;
            }
            set
            {
                moveItemCommand = value;
                RaisePropertyChanged("MoveItemCommand");
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

        public ViewModelShowItemsPage()
        {
            SendCommand = new RelayCommand(ValidateForm, true);
            GoBackCommand = new RelayCommand(NavigateBack, true);
            MoveItemCommand = new RelayCommand<bool>(MoveItem, true);
            ShowItemsDataInListView();
        }

        public void MoveItem(bool option)
        {
            switch(option)
            {
                case true:
                    OrderListData.Add(SelectedItemToAdd);
                    break;
                case false:
                    OrderListData.Remove(SelectedItemToRemove);
                    break;
                default:
                    break;
            }
        }

        private async void ShowItemsDataInListView()
        {
            API api = new API();
            var result = await api.GetAllJsonTask("Item");
            var c = await api.ParseJsonTask(result);

            ItemsData = c;
        }

        public void ValidateForm()
        {
            ErrorMessage = "";

            if (OrderListData.Any())
            {
                AddData();

                ErrorMessage = "Odesláno.";
                SendNotice = Visibility.Visible;
            }
            else
            {
                ErrorMessage = "Žádné položky v košíku. Přidejte alespoň 1 položku!";
                SendNotice = Visibility.Visible;
            }
        }
        public void AddData()
        {
            API api = new API();
            api.InsertOrderData(OrderListData);
        }

        public void NavigateBack()
        {
            NavigationServiceSingleton.GetNavigationService().NavigateBack();
        }
    }
}
