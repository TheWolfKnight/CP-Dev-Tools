using System;
using System.Windows;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using CP_Dev_Tools.Src;
using CP_Dev_Tools.Frontend;
using CP_Dev_Tools.Src.Models;

using static CP_Dev_Tools.Src.FileHandling;

namespace CP_Dev_Tools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string PathToCurrentCase;
        private CanvasManager Manager;
        private Thread SavingThread;

        private bool[] SaveableElements = new bool[3] { false, false, false };


        public MainWindow()
        {
            InitializeComponent();
        }


        private void MainWindow_Init(object sender, EventArgs e)
        {
            Manager = new CanvasManager(this.MapCanvas);
        }

        private void onOpen_Click( object sender, EventArgs e )
        {
            using ( OpenFileDialog fileDialog= new OpenFileDialog() )
            {
                fileDialog.Filter = "JSON file (*.json)| *.json | All files (*.*) | *.*";
                fileDialog.FilterIndex = 1;
                fileDialog.CheckFileExists = true;
                fileDialog.InitialDirectory = @"C:\";

                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Console.WriteLine(fileDialog.FileName);
                    PathToCurrentCase = fileDialog.FileName;
                }
            }
        }




        private void MapCanvas_MouseDown( object sender, MouseButtonEventArgs e)
        {

            if (e.LeftButton != MouseButtonState.Pressed) {
                e.Handled = true;
                return;
            }

            try
            {
                Image target = (Image)e.Source;

                

            }
            catch (InvalidCastException _)
            {
                // Just making sure nothing happens here
            }
            finally
            {
                // Makes sure this does not continue doing something after im done with the edit
                e.Handled = true;
            }
        }


        /// <summary>
        /// Spawns a new saving thread, that can save the current content in the background, while you still work
        /// </summary>
        /// <param name="path"> Path to the desired destination for the saved files </param>
        /// <param name="content"> The content to be saved, NOTE: should be change from a string with the content in it to something better, like making tmp files to house data and copying them </param>
        /// <returns></returns>
        private Thread SpawnNewSaveThread( string path, string content )
        {
            Thread t = new Thread(() => SaveContent(path, content) );
            return t;
        }


        private void Save_Click( object sender, EventArgs e )
        {

            if ( PathToCurrentCase is null )
            {
                SaveAs_Click(sender, e);
                return;
            }

            SavingThread = SpawnNewSaveThread(PathToCurrentCase, /* Need to get the content at this point, TBD */ "");
            SavingThread.Start();
            
        }


        private void SaveAs_Click( object sender, EventArgs e )
        {

            using ( FolderBrowserDialog folderDialog = new FolderBrowserDialog() )
            {
                folderDialog.ShowNewFolderButton = true;

                if ( folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK )
                {
                    if (SavingThread.IsAlive)
                        SavingThread.Abort();

                    string path = folderDialog.SelectedPath;
                    SavingThread = SpawnNewSaveThread(path, /* Need to get the content at this point, TBD */ "");
                    SavingThread.Start();

                }

            }

        }


        /// <summary>
        /// Interfaces with the WriteFile function, saveing the content to the designated path.
        /// Makes sure the program does not overwrite someting unwanted
        /// </summary>
        /// <param name="path"> Path to the desired destination for the saved files </param>
        /// <param name="content"> The content to be saved, NOTE: should be change from a string with the content in it to something better, like making tmp files to house data and copying them </param>
        private void SaveContent( string path, string content)
        {
            if ( !WriteFile( path, content ) )
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("You are about to overwrite a file, are you sure?", "Warning", MessageBoxButton.YesNo);
                if (result != MessageBoxResult.Yes)
                    return;
                WriteFile(path, content, overwrite: true);
            }
        }
        

        public void NewMap_Click( object sender, EventArgs e )
        {
            SizeWindow sizeWindow = new SizeWindow();
            sizeWindow.Caller = this;
            sizeWindow.New = true;
            sizeWindow.Show();
        }

        public void EditChangeMapSize_Click(object sender, EventArgs e)
        {
            SizeWindow sizeWindow = new SizeWindow();
            sizeWindow.Caller = this;
            sizeWindow.New = false;
            sizeWindow.Show();
        }

        public void SizeWindowCall(int[] dims, bool init = false )
        {
            if ( init )
                Manager.DrawInitial(dims);
            else
                Manager.ResizeCanvas(dims);
        }


    }
}
