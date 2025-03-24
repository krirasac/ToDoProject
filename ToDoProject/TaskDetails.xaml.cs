using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Interaction logic for TaskDetails.xaml
    /// </summary>
    public partial class TaskDetails : UserControl
    {
        bool openMenu = false;
        MainWindow home;

        public TaskDetails(MainWindow main)
        {
            InitializeComponent();
            home = main;
        }

        private void MoreBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!openMenu)
            {
                BorderMore.Visibility = Visibility.Visible;
                openMenu = true;
            }

            else if (openMenu)
            {
                BorderMore.Visibility= Visibility.Collapsed;
                openMenu = false;
            }
            
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
           home.MainGrid.Children.Remove(this);
           home.PopBG.Visibility = Visibility.Collapsed;
        }
    }
}
