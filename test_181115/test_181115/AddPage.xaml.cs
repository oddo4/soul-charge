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
    /// Interakční logika pro AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        API api = new API();

        public AddPage()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NameTxtBox.Text.ToString()))
            {
                List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                keyValues.Add(new KeyValuePair<string, string>("Query", "Insert"));
                keyValues.Add(new KeyValuePair<string, string>("Name", NameTxtBox.Text.ToString()));

                var status = await api.InsertData(keyValues, "test");

                RefreshList();

                NavigationService.GoBack();
            }
        }

        public async void RefreshList()
        {
            var json = await api.GetPostsJsonTask("test");
            var result = await api.ParsePostJsonTask(json);

            App.ItemsList = new ObservableCollection<Item>();

            foreach (Item item in result)
            {
                App.ItemsList.Add(item);
            }
        }
    }
}
