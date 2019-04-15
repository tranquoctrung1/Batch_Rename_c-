using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch_Rename
{
    public class RemoveActionArgs : StringArgs, INotifyPropertyChanged
    {
        private string _needle;

        public string Details => $"Remove with {Needle}";

        public string Needle
        {
            get => _needle; set
            {
                _needle = value;
                NotifyChange("Needle");
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

    public class RemoveAction : StringAction
    {
        public string Name => "Remove Action";

        public StringProcesor Procesor => _remove;

        private string _remove(string origin)
        {
            var myArgs = Args as RemoveActionArgs;
            var needle = myArgs.Needle;

            string result = origin;

            while(origin.IndexOf(needle) != -1)
            {
                origin = origin.Replace(needle, "");
            }

            result = origin;

            return result;
        }

        public StringArgs Args { get; set; }

        public StringAction Clone()
        {
            return new RemoveAction()
            {
                Args = new RemoveActionArgs()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new RemoveActionDialog(Args as RemoveActionArgs);

            if (screen.ShowDialog() == true)
            {
                var myArgs = Args as RemoveActionArgs;
                myArgs.Needle = screen.Needle;
            }
        }
    }
}
