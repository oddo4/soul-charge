using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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

namespace chuck_norris
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChuckNorrisAPI chuckNorrisAPI = new ChuckNorrisAPI();
        public MainWindow()
        {
            InitializeComponent();

            ShowJoke();
        }

        public async void ShowJoke()
        {
            ObservableCollection<Joke> col = new ObservableCollection<Joke>();

            if (App.FileHelper.ReadFile() == null)
            {
                for (int i = 0; i < 20; i++)
                {
                    var json = await chuckNorrisAPI.GetPostsJsonTask();
                    var c = await chuckNorrisAPI.ParsePostJsonTask(json);

                    if (col.Count(x => x == c) == 0)
                    col.Add(c);
                }
            }
            else
            {
                col = App.FileHelper.ReadFile();
            }



            TestList.ItemsSource = col;
        }
    }
}
