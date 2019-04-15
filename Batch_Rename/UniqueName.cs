using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Batch_Rename
{
    public class UniqueNameArgs : StringArgs, INotifyPropertyChanged
    {
        private string _needle;
        private string _hammer;

        public string Details => $"Rename {Needle} with {Hammer}";

        public string Needle
        {
            get => _needle; set
            {
                _needle = value;
                NotifyChange("Needle");
                NotifyChange("Details");
            }
        }

        public string Hammer
        {
            get => _hammer; set
            {
                _hammer = value;
                NotifyChange("Hammer");
                NotifyChange("Details");
            }
        }

        private void NotifyChange(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(v));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class UniqueName : StringAction
    {
        public string Name => "Unique Name Action";

        public StringProcesor Procesor => _uniquename;

        private string _uniquename(string origin)
        {
            var myArgs = Args as UniqueNameArgs;
            var needle = myArgs.Needle;
            var hammer = myArgs.Hammer;

            string result = origin.Replace(needle, hammer);

            if (origin != result)
            {
                MessageBox.Show("File name is existed!");
                var screen = new ResolveDuplicateDialog(Args as UniqueNameArgs);

                if (screen.ShowDialog() == true)
                {
                    MessageBox.Show($"We are working on {origin} file");
                    if (screen.Hammer == "1")
                    {
                        for (int i = 1; ; i++)
                        {
                            string[] tokens = hammer.Split(new string[] { "." }, StringSplitOptions.None);
                            string extensions = tokens[tokens.Length - 1];
                            hammer = tokens[0] +$" ({i})"+ "." + extensions;
                            MessageBox.Show(hammer);
                            result = origin.Replace(needle, hammer);
                            if (result != hammer && System.IO.File.Exists(result) == false)
                            {
                                break;
                            }
                            else
                            {
                                if (hammer.IndexOf($" ({i})") != -1)
                                {
                                    hammer = hammer.Replace($" ({i})", "");
                                }
                            }
                        }
                    }
                    else
                    {
                        hammer = screen.Hammer;
                        result = origin.Replace(needle, hammer);
                        while (System.IO.File.Exists(result) == true || result == origin)
                        {
                            MessageBox.Show("File name is existed!");
                            var screen2 = new ResolveDuplicateDialog(Args as UniqueNameArgs);
                            if (screen2.ShowDialog() == true)
                            {
                                hammer = screen2.Hammer;
                                result = origin.Replace(needle, hammer);
                            }
                        }
                    }
                }
            }
            return result;
        }

        public StringArgs Args { get; set; }

        public StringAction Clone()
        {
            return new UniqueName()
            {
                Args = new UniqueNameArgs()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new UniqueNameDialog(Args as UniqueNameArgs);

            if (screen.ShowDialog() == true)
            {
                var myArgs = Args as UniqueNameArgs;
                myArgs.Needle = screen.Needle;
                myArgs.Hammer = screen.Hammer;
            }
        }
    }
}
