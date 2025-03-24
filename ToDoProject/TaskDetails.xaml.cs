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
        FileManager fm;
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
                MoreMenu.Visibility = Visibility.Visible;
                openMenu = true;
            }

            else if (openMenu)
            {
                MoreMenu.Visibility = Visibility.Collapsed;
                openMenu = false;
            }
            
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            ((Grid)this.Parent).Children.Remove(this);
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> listToUpdate = null;
            int index = -1;

            if (parentPage.LowPriorityList.SelectedItem != null)
            {
                index = parentPage.LowPriorityList.SelectedIndex;
                listToUpdate = parentPage.Low;
            }
            else if (parentPage.MediumPriorityList.SelectedItem != null)
            {
                index = parentPage.MediumPriorityList.SelectedIndex;
                listToUpdate = parentPage.Medium;
            }
            else if (parentPage.HighPriorityList.SelectedItem != null)
            {
                index = parentPage.HighPriorityList.SelectedIndex;
                listToUpdate = parentPage.High;
            }
            else if (parentPage.CompletedTasksList.SelectedItem != null)
            {
                index = parentPage.CompletedTasksList.SelectedIndex;
                listToUpdate = parentPage.done;
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
                fm.WriteFile(fm.sList.Select(item => string.Join("|", item)).ToList());
                MessageBox.Show($"Task '{taskName}' has been deleted.");
                ((Grid)this.Parent).Children.Remove(this);

            }
        }

        private Page1 parentPage;
        private void Done_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
