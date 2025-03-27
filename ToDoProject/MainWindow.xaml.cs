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

namespace ToDoProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Button current;
        public Button previous { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new Page1(this);
            changeBG(MW_All_Btn);
            previous = MW_All_Btn;
        }

        private void MW_All_Btn_Click(object sender, RoutedEventArgs e)
        {
            Page1 all = new Page1(this);
            MainFrame.Navigate(all);
            all.CategoryLB.Content = "All Tasks";
            changeBG(sender);
            previous = sender as Button;
        }

        private void MW_Personal_Btn_Click(object sender, RoutedEventArgs e)
        {
            Page1 personal = new Page1(this);
            MainFrame.Navigate(personal);
            personal.CategoryLB.Content = "Personal";
            changeBG(sender);
            previous = sender as Button;
        }
        private void MW_Work_Btn_Click(object sender, RoutedEventArgs e)
        {
            Page1 work = new Page1(this);
            MainFrame.Navigate(work);
            work.CategoryLB.Content = "Work";
            changeBG(sender);
            previous = sender as Button;
        }

        private void MW_Wk_Btn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Page3(this));
            changeBG(sender);
            previous = sender as Button;
        }

        private void MW_Mn_Btn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Page4(this));
            changeBG(sender);
            previous = sender as Button;
        }
        private void changeBG(object sender)
        {
            current = sender as Button;
            current.Foreground = (Brush)new BrushConverter().ConvertFrom("#38A8A3");

            if (previous != null && previous != current)
            {
                previous.Foreground = (Brush)new BrushConverter().ConvertFrom("#F0F0F0");
            }
        }

    }
}
