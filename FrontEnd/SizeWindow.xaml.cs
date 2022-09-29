using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;


namespace CP_Dev_Tools.Frontend
{
    /// <summary>
    /// Interaction logic for ResizeWindow.xaml
    /// </summary>
    public partial class SizeWindow : Window
    {

        public MainWindow Caller { get; set; }
        public bool New { get; set; }

        public SizeWindow()
        {
            InitializeComponent();
        }

        private void acceptButton_Click( object sender, EventArgs _ )
        {
            if (!New)
            {
                MessageBoxResult result = MessageBox.Show($"This action can remove some of your progess if not carful {System.Environment.NewLine}Are you sure?", "Warning", MessageBoxButton.YesNo);

                if (!(result is MessageBoxResult.Yes))
                    this.Close();
            }

            int x, y;
            try
            {
                x = int.Parse(GetRichTextBoxContent(this.xResize));
                y = int.Parse(GetRichTextBoxContent(this.yResize));
                Caller.SizeWindowCall(new int[2] { x, y }, init: New);
                this.Close();
            } catch ( FormatException __ )
            {
                this.errLabel.Visibility = Visibility.Visible;
            }
        }

        private string GetRichTextBoxContent( RichTextBox textBox )
        {
            TextRange range = new TextRange(textBox.Document.ContentStart, textBox.Document.ContentEnd);
            return range.Text;
        }

    }
}
