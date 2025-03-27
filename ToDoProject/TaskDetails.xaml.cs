using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Interaction logic for TaskDetails.xaml
    /// </summary>
    public partial class TaskDetails : UserControl
    {
        bool openMenu = false;
        FileManager fm;
        public MainWindow main { get; set; }
        private Page1 parentPage;

        private string TN;
        private string DT;
        private string DS;
        private string PR;
        private string CT;

        public TaskDetails(Page1 parent, FileManager fm, string name, string date, string desc, string priority, string category)
        {
            InitializeComponent();
            this.fm = fm; // Initialize fm
            parentPage = parent;  // Set the parentPage to the passed-in Page1 reference
            TN = name;
            DT = date;
            DS = desc;
            PR = priority;
            CT = category;
            DisplayTaskContent();
        }

        public void DisplayTaskContent()
        {
            DateTime taskDate = DateTime.Parse(DT);
            string formattedDate = taskDate.ToString("yyyy-MM-dd");

            Name.Content = TN;
            Desc.Text = DS;
            DateTimeLB.Content = formattedDate;  // Display formatted date
            CategoryLB.Content = CT;
            PriorityLB.Content = PR;
        }

        private void MoreBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!openMenu)
            {
                BorderMore.Visibility = Visibility.Visible;
                openMenu = true;
                UpdateDoneButtonContent();
            }
            else
            {
                BorderMore.Visibility = Visibility.Collapsed;
                openMenu = false;
            }
        }
        private void UpdateDoneButtonContent()
        {
            if (parentPage.CompletedTasksList.SelectedItem != null)
                DoneButton.Content = "Mark as Undone";
            else
                DoneButton.Content = "Mark as Done";
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            ((Grid)this.Parent).Children.Remove(this);
            main.PopBG.Visibility = Visibility.Collapsed;
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            Deleting();
            main.PopBG.Visibility = Visibility.Collapsed;
        }

        public void Deleting()
        {
            List<string> listToUpdate = null;
            int index = -1;

            if (parentPage.LowPriorityList.SelectedItem != null)
            {
                index = parentPage.LowPriorityList.SelectedIndex;
                listToUpdate = fm.Low;
            }
            else if (parentPage.MediumPriorityList.SelectedItem != null)
            {
                index = parentPage.MediumPriorityList.SelectedIndex;
                listToUpdate = fm.Medium;
            }
            else if (parentPage.HighPriorityList.SelectedItem != null)
            {
                index = parentPage.HighPriorityList.SelectedIndex;
                listToUpdate = fm.High;
            }
            else if (parentPage.CompletedTasksList.SelectedItem != null)
            {
                index = parentPage.CompletedTasksList.SelectedIndex;
                listToUpdate = fm.done;
            }

            if (index != -1 && listToUpdate != null && index < listToUpdate.Count)
            {
                string taskName = listToUpdate[index];
                listToUpdate.RemoveAt(index);
                parentPage.UpdateListBox();

                for (int i = 0; i < fm.sList.Count; i++)
                {
                    if (fm.sList[i][1] == taskName)
                    {
                        fm.sList.RemoveAt(i);
                        break;
                    }
                }
                fm.RewriteSList();

                if (!deleteTrue)
                {
                    MessageBox.Show($"Task '{taskName}' has been deleted.");
                }

                ((Grid)this.Parent).Children.Remove(this);

            }
        }

        private void Done_Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> listToUpdate = null;
            int index = -1;
            string taskName = "";

            if (parentPage.CompletedTasksList.SelectedItem != null)
            {
                index = parentPage.CompletedTasksList.SelectedIndex;
                listToUpdate = fm.done; 
            }
            else
            {
                if (parentPage.LowPriorityList.SelectedItem != null)
                {
                    index = parentPage.LowPriorityList.SelectedIndex;
                    listToUpdate = fm.Low;
                }
                else if (parentPage.MediumPriorityList.SelectedItem != null)
                {
                    index = parentPage.MediumPriorityList.SelectedIndex;
                    listToUpdate = fm.Medium;
                }
                else if (parentPage.HighPriorityList.SelectedItem != null)
                {
                    index = parentPage.HighPriorityList.SelectedIndex;
                    listToUpdate = fm.High;
                }
            }

            if (index != -1 && listToUpdate != null && index < listToUpdate.Count)
            {
                taskName = listToUpdate[index];
                listToUpdate.RemoveAt(index); 

                if (DoneButton.Content.ToString() == "Mark as Undone")
                {
                    string taskPriority = "";

                    for (int i = 0; i < fm.sList.Count; i++)
                    {
                        if (fm.sList[i][1] == taskName)
                        {
                            taskPriority = fm.sList[i][5];
                            break;
                        }
                    }

                    if (taskPriority == "Low")
                        fm.Low.Add(taskName);
                    else if (taskPriority == "Medium")
                        fm.Medium.Add(taskName);
                    else if (taskPriority == "High")
                        fm.High.Add(taskName);

                    for (int i = 0; i < fm.sList.Count; i++)
                    {
                        if (fm.sList[i][1] == taskName)
                        {
                            fm.sList[i][0] = "-";
                            break;
                        }
                    }
                    MessageBox.Show($"Task '{taskName}' has been marked as undone.");
                }
                else
                {
                    fm.done.Add(taskName);
                    parentPage.UpdateListBox();

                    for (int i = 0; i < fm.sList.Count; i++)
                    {
                        if (fm.sList[i][1] == taskName)
                        {
                            fm.sList[i][0] = "+"; // Mark as done
                            break;
                        }
                    }
                    MessageBox.Show($"Task '{taskName}' has been marked as completed.");
                }
                fm.RewriteSList();
                parentPage.UpdateListBox();
                ((Grid)this.Parent).Children.Remove(this);
                main.PopBG.Visibility = Visibility.Collapsed;
            }
        }


        public static bool deleteTrue = false;

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            ((Grid)this.Parent).Children.Remove(this);
            EditTask edit = new EditTask(parentPage);
            edit.main = main;
            main.MainGrid.Children.Add(edit);
            Grid.SetColumn(edit, 1);

            edit.NameInput.Text = TN;
            edit.CategoryCB.Text = CT;
            edit.DescriptionInput.Text = DS;
            edit.selectedPriority = PR;
        }
    }
}
