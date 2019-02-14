using Microsoft.Win32;
using System;
using System.IO;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using System.Xml.Serialization;

namespace formaty.Pages
{
    /// <summary>
    /// Interakční logika pro MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        OpenFileDialog fDialog;
        SaveFileDialog sDialog;
        IFileHelper fileHelper;
        List<Vehicle> vehicleList;

        public MainPage()
        {
            InitializeComponent();

            
        }

        private void buttonImportFile_Click(object sender, RoutedEventArgs e)
        {
            fDialog = new OpenFileDialog();
            fDialog.Filter = "Supported Files (*.csv,*.json,*.xml)|*.csv;*.json;*.xml";
            fDialog.ShowDialog();

            vehicleList = new List<Vehicle>();

            switch (Path.GetExtension(fDialog.SafeFileName))
            {
                case ".csv":
                    fileHelper = new CsvHelper(fDialog.FileName);;
                    break;
                case ".json":
                    fileHelper = new JsonHelper(fDialog.FileName);
                    break;
                case ".xml":
                    fileHelper = new XmlHelper(fDialog.FileName);
                    break;
            }

            if (fileHelper.ReadFile(vehicleList))
            {
                labelFileStatus.Content = "Úspěšně načteno!";
            }
            else
            {
                labelFileStatus.Content = "Chyba načtení!";
            }

            /*foreach (Vehicle data in vehicleList)
            {
                listViewFileData.Items.Add(new Vehicle { Name = "ahoj", Brand = "svete", LicensePlate = "!!!" });
            }*/

            listViewFileData.ItemsSource = vehicleList;
            
        }

        private void buttonExportFile_Click(object sender, RoutedEventArgs e)
        {
            sDialog = new SaveFileDialog();
            sDialog.Filter = "CSV File (*.csv)|*.csv|JSON File (*.json)|*.json|XML File (*.xml)|*.xml";
            sDialog.ShowDialog();

            switch (Path.GetExtension(sDialog.SafeFileName))
            {
                case ".csv":
                    fileHelper = new CsvHelper(sDialog.FileName);
                    break;
                case ".json":
                    fileHelper = new JsonHelper(sDialog.FileName);
                    break;
                case ".xml":
                    fileHelper = new XmlHelper(sDialog.FileName);
                    break;
            }

            if (fileHelper.WriteFile(vehicleList))
            {
                labelFileStatus.Content = "Úspěšně uloženo!";
            }
            else
            {
                labelFileStatus.Content = "Chyba ukládání!";
            }
        }

        
    }
}
