using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
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

namespace SpotifyAPI
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PrivateProfile profile;
        public MainWindow()
        {
            InitializeComponent();

            Init();
        }

        public async void Init()
        {
            var _clientId = "4dab4bc197084c7db90f8201c5990abe";
            var _secretId = "e5f5c68a50ff48068eb1bfea84cc57a4";

            AuthorizationCodeAuth auth =
        new AuthorizationCodeAuth(_clientId, _secretId, "http://localhost:4002", "http://localhost:4002",
            Scope.PlaylistReadPrivate | Scope.PlaylistReadCollaborative);
            
            auth.Start(); // Starts an internal HTTP Server
            auth.OpenBrowser();

            auth.AuthReceived += async (sender, payload) =>
            {
                auth.Stop();
                Token token = await auth.ExchangeCode(payload.Code);
                SpotifyWebAPI api = new SpotifyWebAPI() { TokenType = token.TokenType, AccessToken = token.AccessToken };
                // Do requests with API client

                profile = await api.GetPrivateProfileAsync();

                if (!profile.HasError())
                {
                    Console.WriteLine(profile.DisplayName);
                }
            };
        }
    }
}
