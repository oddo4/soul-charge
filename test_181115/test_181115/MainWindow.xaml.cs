using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        API api = new API();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendClick(object sender, RoutedEventArgs e)
        {
            var stringQuery = TextBoxQuery.Text;

            GetItems(stringQuery);
        }

        public async void GetItems(string itemID)
        {
            var json =  await api.GetPostsJsonTask(itemID);
            var result =  await api.ParsePostJsonTask(json);

            if (!result.Any())
            {
                LabelInfo.Content = "Položka neexistuje";
            }
            else
            {
                LabelInfo.Content = "";
            }

            ListViewData.ItemsSource = result;
        }
    }
}
