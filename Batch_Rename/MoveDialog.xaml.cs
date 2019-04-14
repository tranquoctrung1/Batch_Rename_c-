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
    /// Interaction logic for MoveDialog.xaml
    /// </summary>
    public partial class MoveDialog : Window
    {
        public string Needle;

        public MoveDialog(MoveArgs args )
        {
            InitializeComponent();

            args.Needle = Needle;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(MoveHeadButton.IsChecked ==true)
            {
                Needle = "Head";
            }
            if(MoveTailButton.IsChecked == true)
            {
                Needle = "Tail";
            }

            this.DialogResult = true;   
        }
    }
}
