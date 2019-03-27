using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
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

namespace WebAPI
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpotifyWebAPI api;
        public MainWindow()
        {
            InitializeComponent();

            api = new SpotifyWebAPI()
            {
                AccessToken = "AQCoW_Ebnc57bKvvE-sAIuu8CimKSQGc1LLyrgpWD0au1LhgOxhODtZTx1TqkFgmb38ctyqu8ABQB3FjiiNjKSi14gGxpgbzMgsG9WlvAPCxMce7wba0C_aYNk3VIf3Ae6iH-fwiE-U0eJcWNxtxI7JZj1a9aW-goEJ1QhoTmouP3W9HujAoT1sQXf54eB1wgz-FhuS1nKEfFbi204qNuo92763cVrgQR9g",
                TokenType = "Bearer"
            };

            Profile();
        }

        public async void Profile()
        {
            PrivateProfile profile = await api.GetPrivateProfileAsync();
            if (!profile.HasError())
            {
                test.Content = profile.DisplayName;
            }
        }
    }
}
