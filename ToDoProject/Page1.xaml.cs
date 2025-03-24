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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        MainWindow main { get; set; }

        public Page1(MainWindow main)
        {
            InitializeComponent();
            this.main = main;

            LowPriorityList.Items.Add("Hello");
            LowPriorityList.Items.Add("Nice to Meet You");
        }

        private void MedAddBTN_Click(object sender, RoutedEventArgs e)
        {
            EditTask selectedTask = new EditTask(main);
            main.MainGrid.Children.Add(selectedTask);

            Grid.SetColumn(selectedTask, 1);
            main.PopBG.Visibility = Visibility.Visible;
        }

        private void ShowCompleteBTN_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button.Content.ToString() == "Show Completed")
            { 
                HighPriority.Visibility = Visibility.Collapsed;
                MediumPriority.Visibility = Visibility.Collapsed;
                LowPriority.Visibility = Visibility.Collapsed;
                CompleteList.Visibility = Visibility.Visible;
                CompleteBorder.Visibility = Visibility.Visible;


                button.Background = (Brush)(new BrushConverter().ConvertFrom("#E65555"));
                button.Content = "Hide Completed";
            }
            else if (button.Content.ToString() == "Hide Completed")
            {
                HighPriority.Visibility = Visibility.Visible;
                MediumPriority.Visibility = Visibility.Visible;
                LowPriority.Visibility = Visibility.Visible;
                CompleteList.Visibility = Visibility.Collapsed;
                CompleteBorder.Visibility = Visibility.Collapsed;

                button.Background = (Brush)(new BrushConverter().ConvertFrom("#38A8A3"));
                button.Content = "Show Completed";
            }
        }

        private void Sample(object sender, MouseButtonEventArgs e)
        {
            TaskDetails selectedTask = new TaskDetails(main);
            main.MainGrid.Children.Add(selectedTask);

            Grid.SetColumn(selectedTask, 1);

            main.PopBG.Visibility = Visibility.Visible;
        }
    }
}
