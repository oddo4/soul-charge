using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace VLC_cviko
{
    public class PlaylistViewModel : ViewModelBase
    {
        private OnlineLinkDialogueWindow linkDialogue;
        private OnlineLinkDialogueViewModel linkDialogueViewModel;

        private ObservableCollection<VideoData> videoList = new ObservableCollection<VideoData>();

        public ObservableCollection<VideoData> VideoList
        {
            get
            {
                return videoList;
            }
            set
            {
                videoList = value;
                RaisePropertyChanged("VideoList");
            }
        }

        private VideoData selectedVideo;

        public VideoData SelectedVideo
        {
            get
            {
                return selectedVideo;
            }
            set
            {
                selectedVideo = value;
                RaisePropertyChanged("SelectedVideo");
            }
        }


        private RelayCommand addFileCommand;

        public RelayCommand AddFileCommand
        {
            get
            {
                return addFileCommand;
            }
            set
            {
                addFileCommand = value;
                RaisePropertyChanged("AddFileCommand");
            }
        }

        private RelayCommand addOnlineCommand;

        public RelayCommand AddOnlineCommand
        {
            get
            {
                return addOnlineCommand;
            }
            set
            {
                addOnlineCommand = value;
                RaisePropertyChanged("AddOnlineCommand");
            }
        }

        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand;
            }
            set
            {
                deleteCommand = value;
                RaisePropertyChanged("DeleteCommand");
            }
        }
        private RelayCommand<object> moveOrderCommand;

        public RelayCommand<object> MoveOrderCommand
        {
            get
            {
                return moveOrderCommand;
            }
            set
            {
                moveOrderCommand = value;
                RaisePropertyChanged("MoveOrderCommand");
            }
        }

        public PlaylistViewModel( )
        {
            AddFileCommand = new RelayCommand(AddFile, true);
            AddOnlineCommand = new RelayCommand(AddOnlineUri, true);
            DeleteCommand = new RelayCommand(Delete, true);
            MoveOrderCommand = new RelayCommand<object>(MoveOrder, true);

            //videoList.Add(new VideoData() { FileName = "online", Path = "http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_h264.mov" } );
        }
        public void AddFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            dlg.Filter = "Video files (*.mp4;*.avi;*.ogg;*.mov)|*.mp4;*.avi;*.ogg;*.mov";

            if (dlg.ShowDialog() == true)
            {
                for (int i = 0; i < dlg.FileNames.Length; i++)
                {
                    VideoData video = new VideoData() { FileName = dlg.SafeFileNames[i], Path = dlg.FileNames[i] };
                    videoList.Add(video);
                } 
            }
        }
        public void AddOnlineUri()
        {
            linkDialogue = new OnlineLinkDialogueWindow();
            linkDialogue.ShowDialog();

            linkDialogueViewModel = linkDialogue.DataContext as OnlineLinkDialogueViewModel;

            if (linkDialogueViewModel.ValidUrl)
            {
                var url = linkDialogueViewModel.LinkUrl;
                var uri = new Uri(url);

                VideoData video = new VideoData() { FileName = System.IO.Path.GetFileName(uri.LocalPath), Path = url };
                videoList.Add(video);
            }
        }
        public void Delete()
        {
            if (selectedVideo != null)
            {
                videoList.Remove(selectedVideo);
            }
        }
        public void MoveOrder(object type)
        {
            var video = selectedVideo;
            var index = videoList.IndexOf(selectedVideo);

            switch (int.Parse(type.ToString()))
            {
                case 0:
                    if (index - 1 >= 0)
                    {
                        index = index - 1;
                        videoList.Remove(selectedVideo);
                        videoList.Insert(index, video);
                    }   
                    break;
                case 1:
                    if (index + 1 <= videoList.Count - 1)
                    {
                        index = index + 1;
                        videoList.Remove(selectedVideo);
                        videoList.Insert(index, video);
                    }
                    break;
                default:
                    break;
            }

            selectedVideo = videoList[index];
        }

    }
}
