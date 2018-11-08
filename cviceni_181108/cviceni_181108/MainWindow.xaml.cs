using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace cviceni_181108
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //int[] intArray1 = new int[3];

            //intArray1[0] = 10;
            //intArray1[1] = 20;
            //intArray1[2] = 30;

            CustomList clist = new CustomList();
            clist.Print();

            clist.Add(10);
            clist.Add(20);

            clist.Print();

            clist.Add(30);

            clist.Print();
        }
    }
}