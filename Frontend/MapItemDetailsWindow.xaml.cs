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
using System.Windows.Shapes;

namespace CP_Dev_Tools.Frontend
{
    /// <summary>
    /// Interaction logic for MapItemDetailsWindow.xaml
    /// </summary>
    public partial class MapItemDetailsWindow : Window
    {

        public FrameworkElement Target;

        public MapItemDetailsWindow( FrameworkElement element )
        {
            Target = element;
        }

        private void MapItemDetailsWindow_Load(object sender, RoutedEventArgs e)
        {
            InitializeComponent();

            switch (Target.Name[0] )
            {
                case 'T':
                    this.Title = "Tile Editor";
                    break;
                case 'D':
                    this.Title = "Decal Editor";
                    break;
            }

        }

        private void EventTree_Click( object sender, MouseButtonEventArgs e )
        {

        } 

    }
}
