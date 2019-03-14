using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VLC_cviko
{
    public class OnlineLinkDialogueViewModel : ViewModelBase
    {
        public bool ValidUrl = false;

        private string linkUrl = "";

        public string LinkUrl
        {
            get
            {
                return linkUrl;
            }
            set
            {
                linkUrl = value;
                RaisePropertyChanged("LinkUrl");
            }
        }

        private string infoText = "";

        public string InfoText
        {
            get
            {
                return infoText;
            }
            set
            {
                infoText = value;
                RaisePropertyChanged("InfoText");
            }
        }

        private RelayCommand<Window> addLinkCommand;

        public RelayCommand<Window> AddLinkCommand
        {
            get
            {
                return addLinkCommand;
            }
            set
            {
                addLinkCommand = value;
                RaisePropertyChanged("AddLinkCommand");
            }
        }

        public OnlineLinkDialogueViewModel()
        {
            AddLinkCommand = new RelayCommand<Window>(CheckFile, true);
        }

        public void CheckFile(Window window)
        {
            try
            {
                string ext = System.IO.Path.GetExtension(linkUrl);

                if (CheckExtension(ext))
                {
                    InfoText = "";
                    ValidUrl = true;
                    window.Close();
                }
                else
                {
                    InfoText = "Wrong video format!";
                }
            }
            catch
            {
                InfoText = "Wrong video format!";
            }
        }

        public bool CheckExtension(string ext)
        {
            string[] validExtensions = { ".mp4", ".avi", ".ogg", ".mov" };

            return validExtensions.Contains(ext.ToLower());
        }

        public void Reset()
        {
            ValidUrl = false;
            LinkUrl = "";
        }
    }
}
