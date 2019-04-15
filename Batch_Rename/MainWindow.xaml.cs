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
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

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
                new RemoveAction(),
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

        private void Loadfoder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog screen = new FolderBrowserDialog();
            if (screen.ShowDialog() == DialogResult.OK)
            {
                string[] dirs = Directory.GetDirectories(screen.SelectedPath);
           
                foreach (string dir in dirs)
                {
                 
                    FolderListView.Items.Add(dir);
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


            foreach (string fodername in FolderListView.Items)
            {
                string result = fodername;
                foreach (StringAction action in ActionListBox.Items)
                {
                    result = action.Procesor.Invoke(result);
                }

                var foder = new FileInfo(fodername);
                foder.MoveTo(result);
            }

            System.Windows.MessageBox.Show("All Done");
        }

        private void RemoveButon_Click(object sender, RoutedEventArgs e)
        {
            if (ActionListBox.SelectedIndex < 0)
            {
                this.Error("Bạn chưa chọn item nào!");
            }
            else
            {
                ActionListBox.Items.RemoveAt(ActionListBox.SelectedIndex);
            }
        }

        List<StringAction> list = null;

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog screen = new OpenFileDialog();

            if(screen.ShowDialog() == true)
            {
                string url = screen.FileName;

                using (StreamWriter wr = new StreamWriter(url))
                {
                    foreach (StringAction action in ActionListBox.Items)
                    {
                        string createText = action.Name + " - " + action.Args.Details;
                        wr.WriteLine(createText);
                        list.Add(action);
                        System.Windows.MessageBox.Show(list.ToString());
                    }
                }
                System.Windows.MessageBox.Show("Finish!");
            }   

        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();

            if(screen.ShowDialog() == true)
            {
                var url = screen.FileName;

                ReplaceAction action = new ReplaceAction()
                {
                };


                using (StreamReader sr = new StreamReader(url))
                {
                    int i = 0;
                    while (sr.Peek() >= 0)
                    {
                        ActionListBox.Items.Add(list[0].Name.ToString());
                    }
                }
            }
            System.Windows.MessageBox.Show("Exported!");
        }
    }
}
