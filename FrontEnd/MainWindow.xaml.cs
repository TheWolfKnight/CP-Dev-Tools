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

using CP_Dev_Tools.Src.WindowHandles;


namespace CP_Dev_Tools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MainWindowHandle Handle { get; set; }


        public MainWindow()
        {
            InitializeComponent();
        }


        private void MainWindow_Init(object sender, EventArgs e)
        {
            Handle = new MainWindowHandle(this);
            Handle.InitMainWindow();
        }

        private void onOpen_Click( object sender, EventArgs e )
        {
            Handle.OpenClickEvent();
        }


        private void MapCanvas_MouseDown( object sender, MouseButtonEventArgs e)
        {
            Handle.MapCanvasMouseDownEvent(e);
        }



        private void Save_Click( object sender, EventArgs e )
        {
            Handle.SaveClickEvent();
        }


        private void SaveAs_Click( object sender, EventArgs e )
        {
            Handle.SaveAsClickEvent();
        }


        public void NewMap_Click( object sender, EventArgs e )
        {
            Handle.NewMapClickEvent();
        }

        public void EditChangeMapSize_Click(object sender, EventArgs e)
        {
            Handle.ChangeSizeClickEvent();
        }

    }
}
