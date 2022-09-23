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

namespace CP_Dev_Tools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void New_Click(object sender, RoutedEventArgs e) { }
        private void Open_Click(object sender, RoutedEventArgs e) { }
        private void RecentFiles_Click(object sender, RoutedEventArgs e) { }
        private void Save_Click(object sender, RoutedEventArgs e) { }
        private void SaveAs_Click(object sender, RoutedEventArgs e) { }
        private void Import_Click(object sender, RoutedEventArgs e) { }
        private void Export_Click(object sender, RoutedEventArgs e) { }

        /// <summary>
        /// Closes the program
        /// </summary>
        /// <param name="sender"> the object that called this function </param>
        /// <param name="e"> the arguments of the call </param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
