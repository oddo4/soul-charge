using Library;
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

namespace highscoreWPF
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<HighScoreData> scoreList = new ObservableCollection<HighScoreData>();
        FileHelper fileHelper = new FileHelper();

        public MainWindow()
        {
            InitializeComponent();
            fileHelper.FilePath = "HighScoreData.csv";
            convertToCollection(fileHelper.ReadCSVFile());
            ScoreList.ItemsSource = scoreList;
        }

        private void SendScore_Click(object sender, RoutedEventArgs e)
        {
            var name = NameInput.Text;
            if (int.TryParse(ScoreInput.Text, out int value))
            {
                HighScoreData score = new HighScoreData() { Name = name, Score = value };
                scoreList.Add(score);

                convertToCollection(scoreList.ToList());

                fileHelper.WriteCSVFile(scoreList.ToList());

                NameInput.Text = "";
                ScoreInput.Text = "";
            }
        }

        private void convertToCollection(List<HighScoreData> listToConvert)
        {
            List<HighScoreData> list = new List<HighScoreData>();
            if (listToConvert != null)
            {
                list = listToConvert.OrderByDescending(x => x.Score).ToList();
            }
                
            scoreList = new ObservableCollection<HighScoreData>();

            foreach (HighScoreData score in list)
            {
                scoreList.Add(score);
            }

            ScoreList.ItemsSource = scoreList;
        }
    }
}
