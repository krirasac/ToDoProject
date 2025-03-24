using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        //CHANGE THE FILE PATH BEFORE RUNNING THE CODE
        private DateTime currentDate = DateTime.Today;
        private string toDoList = "C:\\Users\\Elisha\\source\\repos\\ToDoProject\\ToDoProject\\Assets\\ToDoList.txt";

        public Page4()
        {
            InitializeComponent();
            cal_ToDoCal.SelectedDate = currentDate; //selects the current date
        }


        //populate the stackpanel of tasks based on the selected date
        private void cal_ToDoCal_SelectedDatesChanged(object sender, EventArgs e)
        {
                PopulateToDoList(cal_ToDoCal.SelectedDate.Value);
        }

        private List<(string, string, string, string, string)> FormatTasks(string[] unformattedTasks)
        {
            List<(string name, string time, string date, string category, string priority)> formattedTasks = new List<(string, string, string, string, string)>();

            foreach (string task in unformattedTasks)
            {
                string[] part = task.Split('|');

                string status = part[0];
                string taskName = part[1];
                string taskTime = part[2];
                string taskDate = part[3];
                string category = part[4];
                string priority = part[5];

                formattedTasks.Add((taskName, taskTime, taskDate, category, priority));
            }

            return formattedTasks;
        }

        private void PopulateToDoList(DateTime selectedDate)
        {
            ToDoListPanel.Children.Clear();

            string[] unformattedTasks = File.ReadAllLines(toDoList);
            List<(string name, string time, string date, string category, string priority)> formattedTasks = FormatTasks(unformattedTasks);

            //only the selected date
            List<(string name, string time, string date, string category, string priority)> tasks = new List<(string, string, string, string, string)>();

            foreach ((string, string, string, string, string) task in formattedTasks)
            {
                if (task.Item3 == selectedDate.ToString("yyyy-MM-dd"))
                    tasks.Add(task);
            }

            //add the correct tasks to the stackpanel
            foreach ((string, string, string, string, string) task in tasks)
            {
                Border taskBorder = new Border
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Width = 285,
                    Height = 80,
                    Background = Brushes.Transparent,
                    BorderBrush = Brushes.Transparent,
                    Margin = new Thickness(6)
                };

                Grid taskGrid = new Grid
                {
                    Margin = new Thickness(2),
                };
                taskGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                taskGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15, GridUnitType.Star) });
                taskGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(7, GridUnitType.Star) });
                taskGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                taskGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                Brush color;

                if (task.Item5 == "High")
                    color = Brushes.Red;
                else if (task.Item5 == "Medium")
                    color = Brushes.Yellow;
                else if (task.Item5 == "Low")
                    color = Brushes.Green;
                else
                    color = Brushes.Gray; //no priority given

                Rectangle colorThing = new Rectangle
                {
                    Fill = color,
                    Width = double.NaN,
                    Height = double.NaN,
                };
                Grid.SetRowSpan(colorThing, 2);
                Grid.SetColumn(colorThing, 0);

                TextBlock catName = new TextBlock
                {
                    Text = task.Item4, //wtv u put for the category
                    Foreground = Brushes.LightGray,
                    FontWeight = FontWeights.Bold,
                    FontSize = 14,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(10, 0, 0, 0)
                };
                Grid.SetRow(catName, 0);
                Grid.SetColumn(catName, 1);

                TextBlock taskName = new TextBlock
                {
                    Text = task.Item1, //wtv u put for the task name
                    Foreground = Brushes.Black,
                    FontWeight = FontWeights.Bold,
                    FontSize = 20,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(10, 0, 0, 0)
                };
                Grid.SetRow(taskName, 1);
                Grid.SetColumn(taskName, 1);

                TextBlock time = new TextBlock
                {
                    Text = task.Item2, //time
                    FontSize = 18,
                    TextWrapping = TextWrapping.Wrap,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top
                };
                Grid.SetRow(time, 1);
                Grid.SetColumn(time, 2);
                Grid.SetRowSpan(time, 2);

                taskGrid.Children.Add(colorThing);
                taskGrid.Children.Add(catName);
                taskGrid.Children.Add(taskName);
                taskGrid.Children.Add(time);

                taskBorder.Child = taskGrid;

                ToDoListPanel.Children.Add(taskBorder);
            }
        }

        private void btn_AddTask_Click(object sender, RoutedEventArgs e)
        {
            //navigate to add task page
        }
    }
}
