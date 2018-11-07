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

namespace cviceni_181107
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Node n1 = new Node() { Value = 10 };
            Node n2 = new Node() { Value = 20 };
            Node n3 = new Node() { Value = 30 };

            n1.Pointer = n2;
            n2.Pointer = n3;

            CustomLinkedList clist = new CustomLinkedList();
            clist.Head = n1;

            clist.AddLast(new Node() { Value = 40 });

            //clist.Print();

            CustomStack cstack = new CustomStack() { Head = n1 };
            cstack.Pop();

            cstack.AddFirst(50);

            cstack.Pop();

            CustomQueue cqueue = new CustomQueue() { Head = n1 };
            cqueue.Peek();

            cqueue.AddFirst(60);

            cqueue.Peek();
        }
    }
}
