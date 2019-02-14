using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using xdHelper;

namespace nuget_save
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Vehicle v = new Vehicle();
            v.Name = "Marek";
            v.Brand = "Adamec";
            v.LicensePlate = "260999";

            List<Vehicle> list = new List<Vehicle>();

            list.Add(v);

            JsonHelper jsonHelper = new JsonHelper("xdJS");
            jsonHelper.SaveFilePath = "D:/source/repos/bounlfi15/";

            //jsonHelper.WriteFile(list);

            list = jsonHelper.ReadFile<Vehicle>("D:/source/repos/bounlfi15/xdJS.json");

            listView.ItemsSource = createCol(list);
        }

        private ObservableCollection<T> createCol<T>(List<T> list) where T : class
        {
            ObservableCollection<T> col = new ObservableCollection<T>();

            foreach (T item in list)
            {
                col.Add(item);
            }

            return col;
        }
    }
}
