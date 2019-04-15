using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Batch_Rename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<StringAction> _property = null;

            _property = new List<StringAction>()
            {
                new ReplaceAction(),
                new NewCaseAction(),
                new FullnameNormalizeAction(),
                new MoveAction(),
                new UniqueName(),
            };

            ActionComboBox.ItemsSource = _property;

        }

        static public System.Windows.Forms.DialogResult DialogResult { get; set; }

        private void AddAction_Click(object sender, RoutedEventArgs e)
        {
            var property = ActionComboBox.SelectedItem as StringAction;

            var instance = property.Clone();

            ActionListBox.Items.Add(instance);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var action = ActionListBox.SelectedItem as StringAction;
            action.ShowEditDialog();
        }

        private void LoadFiles_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog screen = new FolderBrowserDialog();

            if(screen.ShowDialog() == DialogResult.OK)
            {
                string[] Files = Directory.GetFiles(screen.SelectedPath);
                foreach(var file in Files)
                {
                    FileListView.Items.Add(file);
                }
                
            }
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(string filename in FileListView.Items)
            {
                string result = filename;
                foreach(StringAction action in ActionListBox.Items)
                {
                    result = action.Procesor.Invoke(result);
                }

                var file = new FileInfo(filename);
                file.MoveTo(result);
            }

            System.Windows.MessageBox.Show("All Done");
        }
    }
}
