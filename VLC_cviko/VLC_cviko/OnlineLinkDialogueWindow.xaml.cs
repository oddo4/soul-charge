﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace VLC_cviko
{
    /// <summary>
    /// Interakční logika pro OnlineLinkDialogueWindow.xaml
    /// </summary>
    public partial class OnlineLinkDialogueWindow : Window
    {
        public OnlineLinkDialogueWindow()
        {
            InitializeComponent();
            this.DataContext = new OnlineLinkDialogueViewModel();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public void Reset()
        {
            ((OnlineLinkDialogueViewModel)DataContext).Reset();
        }
    }
}
