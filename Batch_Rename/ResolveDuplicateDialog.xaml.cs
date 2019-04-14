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
    /// Interaction logic for ResolveDuplicateDialog.xaml
    /// </summary>
    public partial class ResolveDuplicateDialog : Window
    {
        public string Hammer;
        public ResolveDuplicateDialog(UniqueNameArgs args)
        {
            InitializeComponent();

            RenameTextBox.Text = "";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(AutoInscrease.IsChecked == true)
            {
                Hammer = "1";
            }
            if(AutoInscrease.IsChecked == false)
            {
                Hammer = RenameTextBox.Text;
            }

            this.DialogResult = true;
        }
    }
}
