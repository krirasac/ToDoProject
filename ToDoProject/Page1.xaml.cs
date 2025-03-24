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
            PopulateListBox();
        }

        private void PopulateListBox()
        {
            High.Clear(); Medium.Clear(); Low.Clear();
            for (int x = 0; x < fm.sList.Count(); x++)
            {
                if (fm.sList[x][5] == "1")
                {
                    if (fm.sList[x][3] == "High")
                        High.Add(fm.sList[x][0]);
                    else if (fm.sList[x][3] == "Medium")
                        Medium.Add(fm.sList[x][0]);
                    else if (fm.sList[x][3] == "Low")
                        Low.Add(fm.sList[x][0]);
                }
                else if (fm.sList[x][5] == "2")
                {
                    done.Add(fm.sList[x][0]);
                }
            }
            CompletedTasksList.ItemsSource = done;
            HighPriorityList.ItemsSource = High;
            MediumPriorityList.ItemsSource = Medium;
            LowPriorityList.ItemsSource = Low;
        }


        private void MedAddBTN_Click(object sender, RoutedEventArgs e)
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

                button.Content = "Hide Completed";
            }
            else if (button.Content.ToString() == "Hide Completed")
            {
                HighPriority.Visibility = Visibility.Visible;
                MediumPriority.Visibility = Visibility.Visible;
                LowPriority.Visibility = Visibility.Visible;
                CompleteList.Visibility = Visibility.Collapsed;

                button.Content = "Show Completed";
            }
        }

        public void AddTaskToList(EditTask.Task task)
        {
            if (task.Priority == "Low")
            {
                Low.Add(task.Name);
            }
            else if (task.Priority == "Medium")
            {
                Medium.Add(task.Name);
            }
            else if (task.Priority == "High")
            {
                High.Add(task.Name);
            }
            UpdateListBox();  
        }

        private void UpdateListBox()
        {
            HighPriorityList.ItemsSource = null; 
            MediumPriorityList.ItemsSource = null;
            LowPriorityList.ItemsSource = null;

            HighPriorityList.ItemsSource = High;
            MediumPriorityList.ItemsSource = Medium;
            LowPriorityList.ItemsSource = Low;
        }

        private void DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBox LB)
            {
                fm.ReadAndSortFile();
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
                else if (LB.Name == "CompleteleTasksList")
                {
                    index = CompletedTasksList.SelectedIndex;
                }

                if (LB.Name != "CompletedTasksList")
                {
                    for (int i = 0; i < fm.sList.Count; i++)
                    {
                        if (fm.sList[i][3] == LoMeHi)
                            list.Add(fm.sList[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < fm.sList.Count; i++)
                    {
                        if (fm.sList[i][5] == "2")
                            list.Add(fm.sList[i]);
                    }
                }
                    
                main.MainGrid.Children.Add(new TaskDetails(list[index][0], list[index][1], list[index][2], list[index][3], list[index][4]));
            }
        }
    }
}
