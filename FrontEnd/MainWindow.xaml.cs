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

        public int[] ResizeValues { private get; set; }

        private string PathToCurrentCase;
        private CanvasManager Manager;
        private Thread SavingThread;


        public MainWindow()
        {
            InitializeComponent();
        }


        private void MainWindow_Init(object sender, EventArgs e)
        {
            Manager = new CanvasManager(this.MapCanvas);

            Manager.DrawInitial(new int[2] { 25, 30 });

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


        public void EditChangeMapSize_Click( object sender, EventArgs e )
        {
            ResizeWindow resizeWindow = new ResizeWindow();
            resizeWindow.Caller = this;
            resizeWindow.Show();

            if (ResizeValues is null)
                return;

            Manager.ResizeCanvas(ResizeValues);
            ResizeValues = null;
        }


        private void MapCanvas_MouseDown( object sender, MouseButtonEventArgs e)
        {
            if ( e.LeftButton != MouseButtonState.Pressed )
                return;

            // Makes sure this does not continue doing something after im done with the edit
            e.Handled = true;
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
                    if (!(SavingThread is null))
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
            SavingThread = null;
        }
    }
}
