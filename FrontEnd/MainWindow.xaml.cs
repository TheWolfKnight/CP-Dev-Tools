using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using CP_Dev_Tools.Src.Models;

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

        private void onOpen_Click( object sender, EventArgs e )
        {
            using ( OpenFileDialog fileDialog= new OpenFileDialog() )
            {
                fileDialog.Filter = "JSON files (*.json)| *.json | All files (*.*) | *.*";
                fileDialog.FilterIndex = 1;
                fileDialog.CheckFileExists = true;
                fileDialog.InitialDirectory = @"C:\";

                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Console.WriteLine(fileDialog.FileName);
                }
            }
        }

        private void MapCanvas_MouseDown( object sender, MouseButtonEventArgs e)
        {
            if ( e.LeftButton != MouseButtonState.Pressed )
                return;



            // Makes sure this does not get called after im done with the edit
            e.Handled = true;
        }

    }
}
