using FluentValidation.Results;
using formular.Classes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace formular.ViewModels
{
    class ViewModelShowItemsPage : ViewModelBase
    {
        //public LoginValidator validator = new LoginValidator();

        private string username = App.User.Username;

        public string Username
        {
            get { return username; }
            set { username = value; RaisePropertyChanged("Username"); }
        }


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
        private ObservableCollection<Item> itemsData = new ObservableCollection<Item>();

        public ObservableCollection<Item> ItemsData
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
        private ObservableCollection<Item> orderListData = new ObservableCollection<Item>();

        public ObservableCollection<Item> OrderListData
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
        private int selectedIndexToRemove;

        public int SelectedIndexToRemove
        {
            get
            {
                return selectedIndexToRemove;
            }
            set
            {
                selectedIndexToRemove = value;
                RaisePropertyChanged("SelectedIndexToRemove");
            }
        }
        private RelayCommand<object> moveItemCommand;

        public RelayCommand<object> MoveItemCommand
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
            MoveItemCommand = new RelayCommand<object>(MoveItem, true);
            GetDataFromAPI();
        }

        public void MoveItem(object param)
        {
            var option = int.Parse(param.ToString());

            switch(option)
            {
                case 1:
                    if (SelectedItemToAdd != null)
                        OrderListData.Add(SelectedItemToAdd);
                    break;
                case 0:
                    if (SelectedIndexToRemove != -1)
                        OrderListData.RemoveAt(SelectedIndexToRemove);
                    SelectedIndexToRemove = -1;
                    break;
                default:
                    break;
            }
        }

        private async void GetDataFromAPI()
        {
            var result = await App.Api.GetData("Item");
            var c = await App.Api.ParseJsonTask<Item>(result);

            foreach (Item item in c)
            {
                ItemsData.Add(item);
            }
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
        public async void AddData()
        {
            Order newOrder = new Order() { Person_ID = App.User.ID };

            //Debug.WriteLine(DateTime.Now);

            var newOrderSuccess = await App.Api.InsertData(newOrder.CreateKeyValues(), "Order");

            var orderResult = await App.Api.GetData("Order", "id=" + App.User.ID);
            var o = (await App.Api.ParseJsonTask<Order>(orderResult)).Last();

            foreach (Item item in OrderListData)
            {
                var newOrderItems = await App.Api.InsertData(CreateKeyValues(item, o), "OrderItems");
            }
        }

        public List<KeyValuePair<string, string>> CreateKeyValues(Item item, Order order)
        {
            List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

            keyValues.Add(new KeyValuePair<string, string>("Method", "Insert"));
            keyValues.Add(new KeyValuePair<string, string>("Option", "OrderItems"));
            keyValues.Add(new KeyValuePair<string, string>("Order_ID", order.ID.ToString()));
            keyValues.Add(new KeyValuePair<string, string>("Item_ID", item.ID.ToString()));

            return keyValues;
        }

        public void NavigateBack()
        {
            NavigationServiceSingleton.GetNavigationService().NavigateBack();
        }
    }
}
