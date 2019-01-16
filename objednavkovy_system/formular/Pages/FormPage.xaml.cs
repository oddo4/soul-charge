using formular.ViewModels;
using System;
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
using System.Windows.Shapes;

namespace formular.Pages
{
    /// <summary>
    /// Interakční logika pro FormPage.xaml
    /// </summary>
    public partial class FormPage : Page
    {
        public FormPage()
        {
            InitializeComponent();
            this.DataContext = new ViewModelFormPage();
        }
    }
}
