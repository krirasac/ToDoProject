using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        //for the dates in the stackpanel
        private List<Border> dateBorders = new List<Border>();
        private DateTime currentDate = DateTime.Today;
        private string toDoList = "C:\\Users\\Elisha\\source\\repos\\ToDoProject\\ToDoProject\\Assets\\ToDoList.txt";
        public Page3()
        {
            InitializeComponent();
            InitializeDateBorders();
            lbl_Month.Content = currentDate.ToString("MMMM");
            PopulateToDoList(currentDate, 1);
        }

        //start of dates in the stackpanel
        private void InitializeDateBorders()
        {
            DateStackPanel.Children.Clear();
            dateBorders.Clear();

            for (int i = -3; i <= 3; i++)
            {
                DateTime date = currentDate.AddDays(i);

                Border border = CreateDateBorder(date, i == 0);
                border.Tag = i;

                border.MouseLeftButtonDown += Date_Click;

                dateBorders.Add(border);
                DateStackPanel.Children.Add(border);
            }
        }

        private Border CreateDateBorder(DateTime date, bool isCenter)
        {
            TextBlock textBlock = new TextBlock
            {
                Text = $"{date:ddd}\n{date:dd}",
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = isCenter ? Brushes.White : Brushes.Black,
                TextAlignment = TextAlignment.Center
            };

            Border border = new Border
            {
                Width = isCenter ? 100 : 80, //bigger center
                Height = isCenter ? 130 : 90,
                Background = isCenter ? Brushes.Gray : Brushes.LightGray, //ignore the colors first
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(5),
                Child = textBlock,
                Margin = new Thickness(5),
                Cursor = Cursors.Hand
            };

            return border;
        }

        private void Date_Click(object sender, RoutedEventArgs e)
        {
            Border clickedBorder = sender as Border;
            int index = (int)clickedBorder.Tag;

            if (index == 0) return;

            currentDate = currentDate.AddDays(index);
            InitializeDateBorders();

            PopulateToDoList(currentDate, 1);
            lbl_Month.Content = currentDate.ToString("MMMM"); //changes the month label content
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        //end of stackpanel dates

        //forward n backward buttons
        private void ShiftWeek(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Name == "btn_BackDate")
                    currentDate = currentDate.AddDays(-7);
                else if (btn.Name == "btn_ForwDate")
                    currentDate = currentDate.AddDays(7);
            }

            InitializeDateBorders();
            lbl_Month.Content = currentDate.ToString("MMMM");
        }

        //show tasks in the stackpanel
        private List<(string, string, string, string, string, bool)> FormatTasks(string[] unformattedTasks)
        {
            List<(string name, string time, string date, string category, string priority, bool status)> formattedTasks = new List<(string, string, string, string, string, bool)>();

            foreach (string task in unformattedTasks)
            {
                string[] parts = task.Split('|');

                bool status;
                if (parts[0] == "+")
                    status = true;
                else
                    status = false;

                string taskName = parts[1];
                string taskTime = parts[2];
                string taskDate = parts[3];
                string category = parts[4];
                string priority = parts[5];

                formattedTasks.Add((taskName, taskTime, taskDate, category, priority, status));
            }

            return formattedTasks;
        }

        private void PopulateToDoList(DateTime selectedDate, int status)
        {
            //status 1 = all
            //status 2 = ongoing
            //status 3 = completed

            ToDoListPanel.Children.Clear();

            string[] unformattedTasks = File.ReadAllLines(toDoList);
            List<(string name, string time, string date, string category, string priority, bool status)> formattedTasks = FormatTasks(unformattedTasks);

            //only the selected date
            List<(string name, string time, string date, string category, string priority, bool status)> tasks = new List<(string, string, string, string, string, bool)>();

            foreach ((string, string, string, string, string, bool) task in formattedTasks) //all/status=1
            {
                if (task.Item3 == selectedDate.ToString("yyyy-MM-dd"))
                    tasks.Add(task);
            }

            if (status == 2) //ongoing
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    if (tasks[i].Item6 == true)
                    {
                        tasks.Remove(tasks[i]);
                        i--;
                    }
                }
            }
            else if (status == 3) //completed
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    if (tasks[i].Item6 == false)
                    {
                        tasks.Remove(tasks[i]);
                        i--;
                    }
                }
            }

            //add the correct tasks to the stackpanel
            foreach ((string, string, string, string, string, bool) task in tasks)
            {
                Border taskBorder = new Border
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Width = 1055,
                    Height = 100,
                    Background = Brushes.Transparent,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(2),
                    Margin = new Thickness(6),
                    CornerRadius = new CornerRadius(10),
                    VerticalAlignment = VerticalAlignment.Top,
                };

                Grid taskGrid = new Grid
                {
                    Margin = new Thickness(2),
                };
                taskGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });
                taskGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) });
                taskGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });
                taskGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });
                taskGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });
                taskGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                taskGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                TextBlock catName = new TextBlock
                {
                    Text = task.Item4, //wtv u put for the category
                    Foreground = Brushes.LightGray,
                    FontWeight = FontWeights.Bold,
                    FontSize = 18,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(10, 0, 0, 0)
                };
                Grid.SetRow(catName, 1);
                Grid.SetColumn(catName, 1);

                TextBlock taskName = new TextBlock
                {
                    Text = task.Item1, //wtv u put for the task name
                    Foreground = Brushes.Black,
                    FontWeight = FontWeights.Bold,
                    FontSize = 24,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(10, 0, 0, 0)
                };
                Grid.SetRow(taskName, 0);
                Grid.SetColumn(taskName, 1);

                TextBlock priority = new TextBlock
                {
                    Text = task.Item5,
                    FontSize = 24,
                    Foreground= Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(priority, 2);
                Grid.SetRowSpan(priority, 2);

                TextBlock time = new TextBlock
                {
                    Text = task.Item2, //time
                    FontSize = 24,
                    TextWrapping = TextWrapping.Wrap,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(time, 3);
                Grid.SetRowSpan(time, 2);

                TextBlock edit = new TextBlock
                {
                    Text = "Edit",
                    FontSize = 24,
                    Cursor = Cursors.Hand,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                edit.MouseLeftButtonDown += Edit_Click;
                edit.MouseEnter += (s, e) => edit.TextDecorations = TextDecorations.Underline;
                edit.MouseLeave += (s, e) => edit.TextDecorations = null;
                Grid.SetColumn(edit, 4);
                Grid.SetRowSpan(edit, 2);

                CheckBox checkBox = new CheckBox
                {
                    //IsChecked = true,
                    IsChecked = task.Item6 ? true : false,
                    Width = 40,
                    Height = 40,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Cursor = Cursors.Hand
                };
                checkBox.Style = (Style)FindResource("RoundCheckBox");
                Grid.SetColumn(checkBox, 0);
                Grid.SetRowSpan(checkBox, 2);

                //checkBox.Checked += (s, e) => SaveToDoList(checkBox);
                //checkBox.Unchecked += (s, e) => SaveToDoList(checkBox);

                taskGrid.Children.Add(checkBox);
                taskGrid.Children.Add(catName);
                taskGrid.Children.Add(taskName);
                taskGrid.Children.Add(priority);
                taskGrid.Children.Add(time);
                taskGrid.Children.Add(edit);

                taskBorder.Child = taskGrid;

                ToDoListPanel.Children.Add(taskBorder);
            }
        }

        private void SaveToDoList(CheckBox cBox) //FIX THIS REEEEE
        {
            string[] allLines = File.ReadAllLines(toDoList);

            using (StreamWriter sw = new StreamWriter(toDoList, false))
            {
                foreach (string line in allLines)
                {
                    string status = "-";
                    if (cBox.IsPressed) //make this for the specific task
                        status = "+";

                    sw.WriteLine(status + line.Substring(1));
                }
            }
        }

        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                switch (btn.Name)
                {
                    case "btn_All":
                        PopulateToDoList(currentDate, 1);
                        break;
                    case "btn_Ong":
                        PopulateToDoList(currentDate, 2);
                        break;
                    case "btn_Com":
                        PopulateToDoList(currentDate, 3);
                        break;
                }
            }
        }

        private void btn_AddTask_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
