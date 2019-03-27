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
    public class MainWindowViewModel : ViewModelBase
    {
        private VlcControl videoView;
        private PlaylistView playlistView;
        private PlaylistViewModel playlistViewModel;
        private bool isPaused = true;
        private bool isStopped = true;
        private int currentIndex = 0;
        private string[] mediaOptions;

        #region
        private string videoName = "";

        public string VideoName
        {
            get
            {
                return videoName;
            }
            set
            {
                videoName = value;
                RaisePropertyChanged("VideoName");
            }
        }

        private string playbackBtn = "Play";

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
        private RelayCommand nextCommand;

        public RelayCommand NextCommand
        {
            get
            {
                return nextCommand;
            }
            set
            {
                nextCommand = value;
                RaisePropertyChanged("NextCommand");
            }
        }

        private RelayCommand previousCommand;

        public RelayCommand PreviousCommand
        {
            get
            {
                return previousCommand;
            }
            set
            {
                previousCommand = value;
                RaisePropertyChanged("PreviousCommand");
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

        public MainWindowViewModel(VlcControl videoView, PlaylistView playlistView)
        {
            this.videoView = videoView;
            this.playlistView = playlistView;
            this.playlistViewModel = playlistView.DataContext as PlaylistViewModel;

            SetOptions();

            PlaybackCommand = new RelayCommand(Playback, true);
            ForwardCommand = new RelayCommand(Forward, true);
            BackwardCommand = new RelayCommand(Backward, true);
            NextCommand = new RelayCommand(Next, true);
            PreviousCommand = new RelayCommand(Previous, true);
            ShowPlaylistCommand = new RelayCommand(ShowPlaylist, true);

            videoView.MediaPlayer.Playing += VideoPlaying;
            videoView.MediaPlayer.TimeChanged += SliderChanged;
            videoView.MediaPlayer.EndReached += VideoEnd;

            initializeVlcControl();
        }

        public void SetNextVideo()
        {
            ThreadPool.QueueUserWorkItem((v) => videoView.MediaPlayer.Play(playlistViewModel.VideoList[currentIndex].GetUriPath(), mediaOptions));

            VideoName = playlistViewModel.VideoList[currentIndex].FileName;

            Debug.WriteLine(playlistViewModel.VideoList[currentIndex].Path);
        }

        public void SliderChanged(object sender, VlcMediaPlayerTimeChangedEventArgs e)
        {
            VideoTime = videoView.MediaPlayer.Time;

            //Debug.WriteLine(videoView.MediaPlayer.Position + "; " + videoView.MediaPlayer.Time); https://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4
        }

        //Play/pause
        public void Playback()
        {
            if (isStopped)
            {
                if (ExistsVideo())
                {
                    SetNextVideo();
                    isStopped = false;
                    isPaused = true;
                }
                else
                {
                    return;
                }
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
            if (!isStopped)
                videoView.MediaPlayer.Time += 30000;
        }

        //Backward
        public void Backward()
        {
            if (!isStopped)
                videoView.MediaPlayer.Time -= 5000;
        }

        public void Next()
        {
            if (ExistsVideo())
            {
                currentIndex++;
                ResetPlayer();

                if (EndOfPlaylist())
                {
                    currentIndex = 0;
                }

                SetNextVideo();
            }
            else
            {
                Stop();
            }
        }

        public void Previous()
        {
            if (ExistsVideo())
            {
                currentIndex--;
                ResetPlayer();

                if (StartOfPlaylist())
                {
                    currentIndex = playlistViewModel.VideoList.Count - 1;
                }

                SetNextVideo();
            }
            else
            {
                Stop();
            }
        }

        public void Stop()
        {
            isPaused = false;
            Playback();

            isStopped = true;
        }

        public void ShowPlaylist()
        {
            if (playlistView.Visibility == System.Windows.Visibility.Visible)
            {
                playlistView.Hide();
            }
            else
            {
                playlistView.Show();
            }
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
            Next();
        }

        public bool StartOfPlaylist()
        {
            if (currentIndex <= -1)
            {
                return true;
            }

            return false;
        }

        public bool EndOfPlaylist()
        {
            if (currentIndex >= playlistViewModel.VideoList.Count)
            {
                return true;
            }

            return false;
        }

        public bool ExistsVideo()
        {
            if (playlistViewModel.VideoList.Count > 0)
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
