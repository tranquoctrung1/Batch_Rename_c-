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
    /// Interaction logic for UniqueNameDialog.xaml
    /// </summary>
    public partial class UniqueNameDialog : Window
    {
        public string Needle;
        public string Hammer;
        public UniqueNameDialog( UniqueNameArgs args)
        {
            InitializeComponent();
            NeedleTextBox.Text = args.Needle;
            HammerTextBox.Text = args.Hammer;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Needle = NeedleTextBox.Text;
            Hammer = HammerTextBox.Text;
            this.DialogResult = true;
        }
    }
}
