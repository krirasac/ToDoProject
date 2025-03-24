﻿using System;
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
        MainWindow home;

        public EditTask(MainWindow main)
        {
            InitializeComponent();
            home = main;
            NameInput.Text = "cool";
        }

        private void ExitTN_Click(object sender, RoutedEventArgs e)
        {
            home.MainGrid.Children.Remove(this);
            home.PopBG.Visibility = Visibility.Collapsed;
        }
    }
}
