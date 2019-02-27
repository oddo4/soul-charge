using System;
using System.Collections.Generic;
using System.IO;
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

namespace VLC_cviko
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isPaused = false;
        public MainWindow()
        {
            InitializeComponent();

            var vlcLibDirectory = new DirectoryInfo(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

            var options = new string[]
            {
                // VLC options can be given here. Please refer to the VLC command line documentation.
            };

            MyControl.MediaPlayer.VlcLibDirectory = vlcLibDirectory;

            MyControl.MediaPlayer.EndInit();

            // Load libvlc libraries and initializes stuff. It is important that the options (if you want to pass any) and lib directory are given before calling this method.
            MyControl.MediaPlayer.Play(new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_h264.mov"), options);
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            if (isPaused)
            {
                PlayBtn.Content = "▶️";
                MyControl.MediaPlayer.Play();
            }
            else
            {
                PlayBtn.Content = "⏸️";
                MyControl.MediaPlayer.Pause();
            }

            isPaused = !isPaused;
        }
        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            MyControl.MediaPlayer.Position += 10;
        }
        private void Backward_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
