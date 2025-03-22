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
        }

        private void cal_ToDoCal_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateMonthYearLabels();
        }

        private void ShiftMonth(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            int direction = int.Parse(clickedButton.Tag.ToString());

            currentDate = currentDate.AddMonths(direction);
            cal_ToDoCal.DisplayDate = currentDate;
            UpdateMonthYearLabels();
        }

        private void UpdateMonthYearLabels()
        {
            lbl_Month.Content = currentDate.ToString("MMMM");
            lbl_Year.Content = currentDate.ToString("yyyy");
        }

        //date select event = SelectedDatesChanged
        //put it in the xaml like SelectedDatesChanged = "<Event_Name>"
    }
}
