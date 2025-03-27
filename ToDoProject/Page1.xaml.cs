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
        private FileManager fm = new FileManager();
        public List<string> High = new List<string>();
        public List<string> Medium = new List<string>();
        public List<string> Low = new List<string>(); 
        public List<string> done = new List<string>();

        public Page1(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
            fm = new FileManager();
            PopulateListBox();
        }

        private void PopulateListBox()
        {
            High.Clear(); Medium.Clear(); Low.Clear();
            for (int x = 0; x < fm.sList.Count(); x++)
            {
                if (fm.sList[x][0] == "-")
                {
                    if (fm.sList[x][5] == "High")
                        High.Add(fm.sList[x][1]);
                    else if (fm.sList[x][5] == "Medium")
                        Medium.Add(fm.sList[x][1]);
                    else if (fm.sList[x][5] == "Low")
                        Low.Add(fm.sList[x][1]);
                }
                else if (fm.sList[x][0] == "+")
                {
                    done.Add(fm.sList[x][1]);
                }
            }
            CompletedTasksList.ItemsSource = done;
            HighPriorityList.ItemsSource = High;
            MediumPriorityList.ItemsSource = Medium;
            LowPriorityList.ItemsSource = Low;
        }

        private void MedAddBTN_Click(object sender, RoutedEventArgs e)
        {
            EditTask edit = new EditTask(this);
            main.MainGrid.Children.Add(edit);
            main.PopBG.Visibility = Visibility.Visible;
            edit.main = main;
        }

        public void editTaskButon()
        {
            main.MainGrid.Children.Add(new EditTask(this));
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

        public void UpdateListBox()
        {
            HighPriorityList.ItemsSource = null; 
            MediumPriorityList.ItemsSource = null;
            LowPriorityList.ItemsSource = null;
            CompletedTasksList.ItemsSource = null;

            HighPriorityList.ItemsSource = High;
            MediumPriorityList.ItemsSource = Medium;
            LowPriorityList.ItemsSource = Low;
            CompletedTasksList.ItemsSource = done;
        }

        private void DoubleClick(object sender, MouseButtonEventArgs e)
        {
            fm.ReadAndSortFile();

            if (sender is ListBox LB)
            {
                int index = 0;
                string LoMeHi = "";
                List<string[]> list = new List<string[]>();

                if (LB.Name == "LowPriorityList")
                {
                    index = LowPriorityList.SelectedIndex;
                    LoMeHi = "Low";
                }
                else if (LB.Name == "MediumPriorityList")
                {
                    index = MediumPriorityList.SelectedIndex;
                    LoMeHi = "Medium";
                }
                else if (LB.Name == "HighPriorityList")
                {
                    index = HighPriorityList.SelectedIndex;
                    LoMeHi = "High";
                }
                else if (LB.Name == "CompletedTasksList")
                {
                    index = CompletedTasksList.SelectedIndex;
                }

                if (LB.Name != "CompletedTasksList")
                {
                    for (int i = 0; i < fm.sList.Count; i++)
                    {
                        if (fm.sList[i][0] == "-" && fm.sList[i][5] == LoMeHi)
                        {
                            list.Add(fm.sList[i]);
                        }
                            
                    }
                }
                else if (LB.Name == "CompletedTasksList")
                {
                    for (int i = 0; i < fm.sList.Count; i++)
                    {
                        if (fm.sList[i][0] == "+")
                            list.Add(fm.sList[i]);
                    }
                }


                main.MainGrid.Children.Add(new TaskDetails(this, fm, list[index][1], list[index][2], list[index][4], list[index][5], list[index][6]));
            }
        }


    }
}
