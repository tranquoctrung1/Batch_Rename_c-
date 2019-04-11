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
    /// Interaction logic for NewCaseDialog.xaml
    /// </summary>
    public partial class NewCaseDialog : Window
    {
        public string Neelde;

        public NewCaseDialog(NewCaseArgs args)
        {
            InitializeComponent();

            args.Needle = Neelde;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (UppercaseButton.IsChecked == true)
            {
                Neelde = UppercaseButton.Content.ToString();
            }
            else if (LowerCaseButton.IsChecked == true)
            {
                Neelde = LowerCaseButton.Content.ToString();
            }
            else if(Standard.IsChecked == true)
            {
                Neelde = Standard.Content.ToString();
            }
            this.DialogResult = true;
        }
    }
}
