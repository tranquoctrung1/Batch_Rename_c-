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
    /// Interaction logic for RemoveActionDialog.xaml
    /// </summary>
    public partial class RemoveActionDialog : Window
    {
        public string Needle;

        public RemoveActionDialog(RemoveActionArgs args)
        {
            InitializeComponent();

            NeedleTextBox.Text = args.Needle;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Needle = NeedleTextBox.Text;

            this.DialogResult = true;
        }
    }
}
