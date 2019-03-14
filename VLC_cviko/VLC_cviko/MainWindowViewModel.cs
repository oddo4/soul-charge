using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vlc.DotNet.Core;
using Vlc.DotNet.Wpf;

namespace VLC_cviko
{
    class MainWindowViewModel : ViewModelBase
    {
        private VlcControl videoView;
        private bool isPaused = false;
        private bool isStopped = false;
        private List<Uri> uriList = new List<Uri>();
        private int currentIndex = 0;
        private string[] mediaOptions;

        #region
        private string playbackBtn = "Pause";

        public string PlaybackBtn
        {
            get
            {
                return playbackBtn;
            }
            set
            {
                playbackBtn = value;
                RaisePropertyChanged("PlaybackBtn");
            }
        }

        private float videoTime;

        public float VideoTime
        {
            get
            {
                return videoTime;
            }
            set
            {
                videoTime = value;
                RaisePropertyChanged("VideoTime");
            }
        }

        private float videoMaxTime = 1;

        public float VideoMaxTime
        {
            get
            {
                return videoMaxTime;
            }
            set
            {
                videoMaxTime = value;
                RaisePropertyChanged("VideoMaxTime");
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
        private RelayCommand showPlaylistCommand;

        public RelayCommand ShowPlaylistCommand
        {
            get
            {
                return showPlaylistCommand;
            }
            set
            {
                showPlaylistCommand = value;
                RaisePropertyChanged("ShowPlaylistCommand");
            }
        }
        #endregion

        public MainWindowViewModel(VlcControl videoView)
        {
            CreateList();

            this.videoView = videoView;

            SetOptions();

            PlaybackCommand = new RelayCommand(Playback, true);
            ForwardCommand = new RelayCommand(Forward, true);
            BackwardCommand = new RelayCommand(Backward, true);
            ShowPlaylistCommand = new RelayCommand(ShowPlaylist, true);

            videoView.MediaPlayer.Playing += VideoPlaying;
            videoView.MediaPlayer.TimeChanged += SliderChanged;
            videoView.MediaPlayer.EndReached += VideoEnd;

            initializeVlcControl();

            SetNextVideo();
        }

        public void CreateList()
        {
            uriList.Add(new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_h264.mov"));
            uriList.Add(new Uri("D:/source/repos/bounlfi15/soul-charge/VLC_cviko/BigBuckBunny_320x180.mp4"));
        }

        public void SetNextVideo()
        {
            ThreadPool.QueueUserWorkItem((v) => videoView.MediaPlayer.Play(uriList[currentIndex], mediaOptions));

            Debug.WriteLine("next " + currentIndex);
        }

        public void SliderChanged(object sender, VlcMediaPlayerTimeChangedEventArgs e)
        {
            VideoTime = videoView.MediaPlayer.Time;

            //Debug.WriteLine(videoView.MediaPlayer.Position + "; " + videoView.MediaPlayer.Time);
        }

        //Play/pause
        public void Playback()
        {
            if (isStopped)
            {
                SetNextVideo();
                isStopped = false;
                isPaused = false;
            }

            if (isPaused)
            {
                videoView.MediaPlayer.Play();
                PlaybackBtn = "Pause";
            }
            else
            {
                videoView.MediaPlayer.Pause();
                PlaybackBtn = "Play";
            }

            isPaused = !isPaused;
        }

        //Forward
        public void Forward()
        {
            videoView.MediaPlayer.Time += 30000;
        }

        //Backward
        public void Backward()
        {
            videoView.MediaPlayer.Time -= 5000;
        }

        public void ShowPlaylist()
        {
            PlaylistView playlist = new PlaylistView();
        }

        public void Rewind()
        {
            videoView.MediaPlayer.Time = (long)VideoTime;
        }

        private void VideoPlaying(object sender, VlcMediaPlayerPlayingEventArgs e)
        {
            VideoMaxTime = videoView.MediaPlayer.Length;
        }

        public void VideoEnd(object sender, VlcMediaPlayerEndReachedEventArgs e)
        {
            currentIndex++;
         
            ResetPlayer();

            if (!EndOfPlaylist())
            {
                SetNextVideo();
            }
            else
            {
                isPaused = false;
                Playback();

                ResetPlaylist();

                isStopped = true;
                videoView.MediaPlayer.Pause();
            }
        }

        public bool EndOfPlaylist()
        {
            if (currentIndex >= uriList.Count)
            {
                return true;
            }

            return false;
        }

        private void initializeVlcControl()
        {
            var vlcLibDirectory = new DirectoryInfo(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

            videoView.MediaPlayer.VlcLibDirectory = vlcLibDirectory;


            videoView.MediaPlayer.EndInit();
        }

        public void SetOptions()
        {
            //mediaOptions = new string[] { "input-repeat=1" };
        }

        public void ResetPlayer()
        {
            VideoTime = 0;
        }
        public void ResetPlaylist()
        {
            currentIndex = 0;
        }
    }
}
