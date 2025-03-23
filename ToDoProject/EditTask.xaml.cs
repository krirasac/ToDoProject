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
    /// Interaction logic for EditTask.xaml
    /// </summary>
    public partial class EditTask : UserControl
    {
        public class Task
        {
            public string Name { get; set; }
            public DateTime Deadline { get; set; }
            public string Category { get; set; }
            public string Priority { get; set; }
            public string Description { get; set; }

            public Task(string name, DateTime deadline, string category, string priority, string description)
            {
                Name = name;
                Deadline = deadline;
                Category = category;
                Priority = priority;
                Description = description;
            }
        }

        private Page1 parentPage;
        public EditTask(Page1 parent)
        {
            InitializeComponent();
            this.parentPage = parent;
            CategoryCB.Items.Add("No Category");
            CategoryCB.Items.Add("Personal");
            CategoryCB.Items.Add("Work");
        }

        private string selectedPriority = "None";

        private FileManager fm = new FileManager();

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            string name = NameInput.Text;
            DateTime deadline = DeadlinePicker.SelectedDate ?? DateTime.MinValue;
            string category = CategoryCB.SelectedItem != null ? CategoryCB.SelectedItem.ToString() : "No Category";
            string description = DescriptionInput.Text;

            Task newTask = new Task(name, deadline, category, selectedPriority, description);

            parentPage.AddTaskToList(newTask);

            MessageBox.Show("Task Saved:\n" +
                            "Name: " + newTask.Name + "\n" +
                            "Deadline: " + newTask.Deadline + "\n" +
                            "Category: " + newTask.Category + "\n" +
                            "Priority: " + newTask.Priority + "\n" +
                            "Description: " + newTask.Description);

            fm.list.Add($"{newTask.Name}|{newTask.Deadline}|{newTask.Category}|{newTask.Priority}|{newTask.Description}");
            fm.WriteFile(fm.list);
            if (newTask.Priority == "High")
                parentPage.High.Add(newTask.Name);
            else if (newTask.Priority == "Medium")
                parentPage.Medium.Add(newTask.Name);
            else if (newTask.Priority == "Low")
                parentPage.Low.Add(newTask.Name);
            
        }

        private void LowBTN_Click(object sender, RoutedEventArgs e)
        {
            selectedPriority = "Low";
            SetPriorityButtonStyles(LowBTN);
        }

        private void MedBTN_Click(object sender, RoutedEventArgs e)
        {
            selectedPriority = "Medium";
            SetPriorityButtonStyles(MedBTN);
        }

        private void HighBTN_Click(object sender, RoutedEventArgs e)
        {
            selectedPriority = "High";
            SetPriorityButtonStyles(HighBTN);
        }

        private void SetPriorityButtonStyles(Button selectedButton)
        {
            LowBTN.Background = Brushes.LightGray;
            MedBTN.Background = Brushes.LightGray;
            HighBTN.Background = Brushes.LightGray;

            selectedButton.Background = Brushes.LightBlue; // Highlight selected button
        }

        private void ExitTN_Click(object sender, RoutedEventArgs e)
        {
            ((Grid)this.Parent).Children.Remove(this);
        }
    }
}
