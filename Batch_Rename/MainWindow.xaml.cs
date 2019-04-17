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

                using (StreamReader sr = new StreamReader(url))
                {
                    while (sr.Peek() >= 0)
                    {
                        string[] tokens_sub = sr.ReadLine().Split(new string[] { " - " }, StringSplitOptions.None);
                        string[] tokens_space = tokens_sub[0].Split(new string[] { " " }, StringSplitOptions.None);
                        string[] tokens_Args = tokens_sub[1].Split(new string[] { " " }, StringSplitOptions.None);

                        string first_word = tokens_space[0];

                        if(first_word == "Replace" )
                        {                         
                            var _replaceArgs = new ReplaceArgs();
                            _replaceArgs.Needle = tokens_Args[1];
                            _replaceArgs.Hammer = tokens_Args[3];

                            var _action  = new ReplaceAction();

                            var _replacAction = _action as StringAction;

                            var _cloneReplaceAction = _replacAction.Clone();
                            _cloneReplaceAction.Args = _replaceArgs;

                            ActionListBox.Items.Add(_cloneReplaceAction);
                        }
                        else if(first_word == "New")
                        {
                            var _newcaseArgs = new NewCaseArgs();
                            _newcaseArgs.Needle = tokens_Args[3];

                            var _action = new NewCaseAction();

                            var _newcaseAction = _action as StringAction;

                            var _cloneNewCaseAction = _newcaseAction.Clone();
                            _cloneNewCaseAction.Args = _newcaseArgs;

                            ActionListBox.Items.Add(_cloneNewCaseAction);
                        }
                        else if(first_word == "Fullname")
                        {
                            var _fullnameArgs = new FullnameNormalizeArgs();
                            _fullnameArgs.Needle = tokens_Args[3];

                            var _action = new FullnameNormalizeAction();

                            var _fullnameAction = _action as StringAction;

                            var _cloneFullNameAction = _fullnameAction.Clone();
                            _cloneFullNameAction.Args = _fullnameArgs;

                            ActionListBox.Items.Add(_cloneFullNameAction);
                        }
                        else if(first_word == "Move")
                        {
                            var _moveArgs = new MoveArgs();
                            _moveArgs.Needle = tokens_Args[2];

                            var _action = new MoveAction();

                            var _moveAction = _action as StringAction;

                            var _cloneMoveAction = _moveAction.Clone();
                            _cloneMoveAction.Args = _moveArgs;

                            ActionListBox.Items.Add(_cloneMoveAction);
                        }
                        else if(first_word == "Unique")
                        {
                            var _uniqueArgs = new UniqueNameArgs();
                            _uniqueArgs.Needle = tokens_Args[1];
                            _uniqueArgs.Hammer = tokens_Args[3];

                            var _action = new UniqueName();

                            var _uniqueAction = _action as StringAction;

                            var _cloneUniqueAction = _uniqueAction.Clone();
                            _cloneUniqueAction.Args = _uniqueArgs;

                            ActionListBox.Items.Add(_cloneUniqueAction);
                        }
                        else
                        {
                            var _removeArgs = new RemoveActionArgs();
                            _removeArgs.Needle = tokens_Args[2];

                            var _action = new RemoveAction();

                            var _removeAction = _action as StringAction;

                            var _cloneRemoveAction = _removeAction.Clone();
                            _cloneRemoveAction.Args = _removeArgs;

                            ActionListBox.Items.Add(_cloneRemoveAction);
                        }
                    }
                }
                System.Windows.MessageBox.Show("Exported!");
            }
        }
    }
}
