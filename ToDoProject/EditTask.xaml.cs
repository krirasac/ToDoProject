using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

        public MainWindow main { get; set; }
        private Page1 parentPage;
        public EditTask(Page1 parent)
        {
            InitializeComponent();
            this.parentPage = parent;
            CategoryCB.Items.Add("No Category");
            CategoryCB.Items.Add("Personal");
            CategoryCB.Items.Add("Work");
        }

        public EditTask(Page3 parent)
        {
            InitializeComponent();
            CategoryCB.Items.Add("No Category");
            CategoryCB.Items.Add("Personal");
            CategoryCB.Items.Add("Work");
        }

        public EditTask(Page4 parent)
        {
            InitializeComponent();
            CategoryCB.Items.Add("No Category");
            CategoryCB.Items.Add("Personal");
            CategoryCB.Items.Add("Work");
        }



        public string selectedPriority = "None";

        private FileManager fm = new FileManager();

        private string UniqueTaskName(string taskName)
        {
            int counter = 1;
            string uniqueName = taskName;
            while (fm.High.Contains(uniqueName) || fm.Medium.Contains(uniqueName) || fm.Low.Contains(uniqueName) || fm.done.Contains(uniqueName))
            {
                uniqueName = $"{taskName} ({counter})";
                counter++;
            }
            return uniqueName;
        }


        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {

            string name = NameInput.Text;
            DateTime deadline = DeadlinePicker.SelectedDate ?? DateTime.MinValue;
            DateTime dateOnly = deadline.Date;
            string formattedDate = dateOnly.ToString("yyyy-MM-dd");  
            string category = CategoryCB.SelectedItem != null ? CategoryCB.SelectedItem.ToString() : "No Category";
            string description = DescriptionInput.Text;

            string uniqueName = UniqueTaskName(name);
            Task newTask = new Task(uniqueName, dateOnly, category, selectedPriority, description);

            MessageBox.Show("Task Saved:\n" +
                            "Name: " + newTask.Name + "\n" +
                            "Deadline: " + newTask.Deadline + "\n" +
                            "Category: " + newTask.Category + "\n" +
                            "Priority: " + newTask.Priority + "\n" +
                            "Description: " + newTask.Description);

            fm.list.Add($"-|{newTask.Name}|11:59 PM|{formattedDate}|{newTask.Category}|{newTask.Priority}|{newTask.Description}");
            fm.WriteFile(fm.list);
            if (newTask.Priority == "High")
                fm.High.Add(newTask.Name);
            else if (newTask.Priority == "Medium")
                fm.Medium.Add(newTask.Name);
            else if (newTask.Priority == "Low")
                fm.Low.Add(newTask.Name);

            ((Grid)this.Parent).Children.Remove(this);
            main.PopBG.Visibility = Visibility.Collapsed;

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
           Button[] BTNList = new Button[] {LowBTN, MedBTN, HighBTN };
            string[] BTNColor = new string[] { "#FF72ABA8", "#FFC99F32", "#FFCE533E" };
            
            for(int x = 0; x < BTNList.Length; x++)
            { 
                if(selectedButton != BTNList[x])
                { 
                    BTNList[x].Background = Brushes.LightGray;
                }
                else
                {
                    var convert = new BrushConverter();
                    selectedButton.Background = (SolidColorBrush)convert.ConvertFromString(BTNColor[x]);
                }
            }
        }

        private void ExitTN_Click(object sender, RoutedEventArgs e)
        {
            main.PopBG.Visibility = Visibility.Collapsed;
            ((Grid)this.Parent).Children.Remove(this);
        }
    }
}
