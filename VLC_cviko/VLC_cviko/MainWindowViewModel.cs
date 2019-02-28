using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vlc.DotNet.Core;
using Vlc.DotNet.Wpf;

namespace VLC_cviko
{
    class MainWindowViewModel : ViewModelBase
    {
        private VlcControl videoView;
        private bool isPaused = false;

        private double videoTime;

        public double VideoTime
        {
            get
            {
                return videoTime;
            }
            set
            {
                videoTime = value;
                RaisePropertyChanged("VideoTime");
                KingCrimson(videoTime);
            }
        }


        private RelayCommand playbackCommand;

        public RelayCommand PlaybackCommand
        {
            get
            {
                return playbackCommand;
            }
            set
            {
                playbackCommand = value;
                RaisePropertyChanged("PlaybackCommand");
            }
        }

        private RelayCommand forwardCommand;

        public RelayCommand ForwardCommand
        {
            get
            {
                return forwardCommand;
            }
            set
            {
                forwardCommand = value;
                RaisePropertyChanged("ForwardCommand");
            }
        }

        private RelayCommand backwardCommand;

        public RelayCommand BackwardCommand
        {
            get
            {
                return backwardCommand;
            }
            set
            {
                backwardCommand = value;
                RaisePropertyChanged("BackwardCommand");
            }
        }

        public MainWindowViewModel(VlcControl videoView)
        {
            this.videoView = videoView;

            initializeVlcControl();

            PlaybackCommand = new RelayCommand(TheWorld, true);
            ForwardCommand = new RelayCommand(MadeInHeaven, true);
            BackwardCommand = new RelayCommand(Mandom, true);
            videoView.MediaPlayer.TimeChanged += GoldExperienceRequiem;


            videoView.MediaPlayer.Play(new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_h264.mov"));
        }

        private void GoldExperienceRequiem(object sender, VlcMediaPlayerTimeChangedEventArgs e)
        {
            VideoTime = videoView.MediaPlayer.Time;
            Debug.WriteLine(VideoTime);
        }

        private void TheWorld()
        {
            if (isPaused)
            {
                videoView.MediaPlayer.Play();
            }
            else
            {
                videoView.MediaPlayer.Pause();
            }

            isPaused = !isPaused;
        }

        private void MadeInHeaven()
        {
            //videoView.MediaPlayer.Time
        }

        private void Mandom()
        {

        }

        private void KingCrimson(double value)
        {

        }

        private void initializeVlcControl()
        {
            var vlcLibDirectory = new DirectoryInfo(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

            videoView.MediaPlayer.VlcLibDirectory = vlcLibDirectory;

            videoView.MediaPlayer.EndInit();
        }
    }
}
