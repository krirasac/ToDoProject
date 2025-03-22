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
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        //for the dates in the stackpanel
        private List<Border> dateBorders = new List<Border>();
        private DateTime currentDate = DateTime.Today;
        public Page3()
        {
            InitializeComponent();
            InitializeDateBorders();
            lbl_Month.Content = currentDate.ToString("MMMM");
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

        private void btn_AddTask_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
