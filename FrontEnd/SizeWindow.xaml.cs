using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;


namespace CP_Dev_Tools.FrontEnd
{
    /// <summary>
    /// Interaction logic for ResizeWindow.xaml
    /// </summary>
    public partial class ResizeWindow : Window
    {

        public MainWindow Caller { get; set; }

        public ResizeWindow()
        {
            InitializeComponent();
        }

        private void acceptButton_Click( object sender, EventArgs _ )
        {
            int x, y;
            try
            {
                x = int.Parse(GetContent(this.xResize));
                y = int.Parse(GetContent(this.yResize));
                Caller.ResizeValues = new int[] { x, y };

            } catch ( FormatException __ )
            {
                this.errLabel.Visibility = Visibility.Visible;
            }
        }

        private string GetContent( RichTextBox textBox )
        {
            TextRange range = new TextRange(textBox.Document.ContentStart, textBox.Document.ContentEnd);
            return range.Text;
        }


    }
}
