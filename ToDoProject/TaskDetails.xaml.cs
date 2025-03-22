﻿using System;
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

        public TaskDetails(EditTask.Task selectedTask)
        {
            InitializeComponent();
            DisplayTaskDetails(selectedTask);
        }

        private void DisplayTaskDetails(EditTask.Task task)
        {
            if (task == null) return;

            DateTimeLB.Content = task.Deadline.ToShortDateString();
            CategoryLB.Content = task.Category;
            PriorityLB.Content = task.Priority;
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
    }
}
