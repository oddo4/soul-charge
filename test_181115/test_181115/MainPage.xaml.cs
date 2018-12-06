using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace test_181115
{
    /// <summary>
    /// Interakční logika pro MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        API api = new API();
        public MainPage()
        {
            InitializeComponent();
            GetItems();
        }

        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPage());
        }
        private void EditPerson_Click(object sender, RoutedEventArgs e)
        {
            var person = (Item)ListViewData.SelectedItem;
            if (person != null)
                NavigationService.Navigate(new EditPage(person));
        }

        private void DeletePerson_Click(object sender, RoutedEventArgs e)
        {
            var person = (Item)ListViewData.SelectedItem;
            if (person != null)
            {
                List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                keyValues.Add(new KeyValuePair<string, string>("Query", "Delete"));
                keyValues.Add(new KeyValuePair<string, string>("Person_ID", person.ID.ToString()));

                api.InsertData(keyValues, "test");

                App.ItemsList.Remove(person);
            }
                
        }

        public async void GetItems(string itemID = "")
        {
            var json = await api.GetPostsJsonTask("test");
            var result = await api.ParsePostJsonTask(json);

            App.ItemsList = new ObservableCollection<Item>();

            foreach (Item item in result)
            {
                App.ItemsList.Add(item);
            }

            ListViewData.ItemsSource = App.ItemsList;
        }
    }
}
