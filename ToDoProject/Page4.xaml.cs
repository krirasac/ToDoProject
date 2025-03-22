using System;
using System.Collections.Generic;
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
        private DateTime currentDate = DateTime.Today;

        public Page4()
        {
            InitializeComponent();
            cal_ToDoCal.SelectedDate = currentDate; //selects the current date
        }

        //show the dots
        private void cal_ToDoCal_Loaded(object sender, RoutedEventArgs e)
        {
            
        }


        //populate the stackpanel of tasks based on the selected date
        private void cal_ToDoCal_SelectedDatesChanged(object sender, EventArgs e)
        {
                PopulateToDoList(cal_ToDoCal.SelectedDate.Value);
        }

        private void PopulateToDoList(DateTime date)
        {
            ToDoListPanel.Children.Clear();

            //put the code in a for/foreach loop
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

            Rectangle colorThing = new Rectangle
            {
                Fill = Brushes.MediumPurple, //is this for the priority?
                Width = double.NaN,
                Height = double.NaN,
            };
            Grid.SetRowSpan(colorThing, 2);
            Grid.SetColumn(colorThing, 0);

            TextBlock catName = new TextBlock
            {
                Text = "Category", //wtv u put for the category
                Foreground = Brushes.LightGray,
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(10, 0, 0, 0)
            };
            Grid.SetRow(catName, 0);
            Grid.SetColumn(catName, 1);

            TextBlock taskName = new TextBlock
            {
                Text = "Task Name", //wtv u put for the task name
                Foreground= Brushes.Black,
                FontWeight = FontWeights.Bold,
                FontSize = 24,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(10, 0, 0, 0)
            };
            Grid.SetRow(taskName, 1);
            Grid.SetColumn(taskName, 1);

            TextBlock time = new TextBlock
            {
                Text = "12:00", //time
                FontSize = 22,
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

        private void btn_AddTask_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
