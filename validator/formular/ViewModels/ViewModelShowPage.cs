using formular.Classes;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formular.ViewModels
{
    class ViewModelShowPage : ViewModelBase
    {
        private List<Person> resultData;

        public List<Person> ResultData
        {
            get
            {
                return resultData;
            }
            set
            {
                resultData = value;
                RaisePropertyChanged("ResultData");
            }
        }

        public ViewModelShowPage()
        {
            ShowDataInListView();
        }

        private async void ShowDataInListView()
        {
            API api = new API();
            var result = await api.GetPostsJsonTask("");
            var c = await api.ParsePostJsonTask(result);

            ResultData = c;
        }
    }
}
