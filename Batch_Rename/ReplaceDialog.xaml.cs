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

namespace Batch_Rename
{
    /// <summary>
    /// Interaction logic for ReplaceDialog.xaml
    /// </summary>
    public partial class ReplaceDialog : Window
    {
        public string Needle;
        public string Hammer;

        public ReplaceDialog(ReplaceArgs args)
        {
            InitializeComponent();
            needleTextBox.Text = args.Needle;
            hammerTextBox.Text = args.Hammer;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Needle = needleTextBox.Text;
            Hammer = hammerTextBox.Text;

            this.DialogResult = true;
        }
    }
}
