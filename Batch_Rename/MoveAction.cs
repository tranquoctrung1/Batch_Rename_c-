using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch_Rename
{
    public class MoveArgs : StringArgs, INotifyPropertyChanged
    {
        private string _needle;



        public string Details => $"{Needle}";

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
    public class MoveAction: StringAction
    {
        public string Name => "Move Action";

        public StringProcesor Procesor => _moveon;

        private string _moveon(string origin)
        {
            string[] tokens = origin.Split(new string[] { "\\" }, StringSplitOptions.None);
            string[] tokendots = tokens[tokens.Length - 1].Split(new string[] { "." }, StringSplitOptions.None);
            string extensions = tokendots[tokendots.Length - 1];

            string[] StringChar = tokendots[0].Split(new string[] { " " }, StringSplitOptions.None);

            string temp = null;

            foreach(string index in StringChar)
            {
                if(index.Length == 13)
                {
                    char firstchar = index[0];
                    if(firstchar >= '0' && firstchar <= '9')
                    {
                        temp = index;
                    }
                }
            }

            while(tokendots[0].IndexOf(temp) != 1)
            {
                tokendots[0] = tokendots[0].Replace(temp, "");
            }


            return null;
        }

        public StringArgs Args { get; set; }

        public StringAction Clone()
        {
            return new MoveAction()
            {
                Args = new MoveArgs()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new MoveDialog(Args as MoveArgs);

            if (screen.ShowDialog() == true)
            {
                var myArgs = Args as MoveArgs;
                myArgs.Needle = screen.Needle;
            }
        }
    }
}
