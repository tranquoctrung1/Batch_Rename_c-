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
    /// Interaction logic for FullnameNormalizeDialog.xaml
    /// </summary>
    public partial class FullnameNormalizeDialog : Window
    {
        public string Needle;

        public FullnameNormalizeDialog(FullnameNormalizeArgs args)
        {
            InitializeComponent();

            args.Needle = Needle;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(NoneSpace.IsChecked == true)
            {
                Needle = NoneSpace.Name;

            }
            if(Standard.IsChecked == true)
            {
                Needle = Standard.Name;
            }
            if(OneSpace.IsChecked == true)
            {
                Needle = OneSpace.Name;
            }
            this.DialogResult = true;
        }
    }
}
