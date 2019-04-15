using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Batch_Rename
{
    public class FullnameNormalizeArgs : StringArgs, INotifyPropertyChanged
    {
        private string _needle;



        public string Details => $"Fullname Normalize with {Needle}";

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

    public class FullnameNormalizeAction : StringAction
    {
        public string Name => "Fullname Normalize Action";

        public StringProcesor Procesor => _normalize;


        private string _normalize(string origin)
        {
            var myArgs = Args as FullnameNormalizeArgs;
            var needle = myArgs.Needle;

            string result = null;


            string[] tokens = origin.Split(new string[] { "\\" }, StringSplitOptions.None);
            string[] tokendots = tokens[tokens.Length - 1].Split(new string[] { "." }, StringSplitOptions.None);
            string extensions = tokendots[tokendots.Length - 1];
            tokendots[0] = tokendots[0].Trim();

            string StringFinal = null;

            if (needle == "NoneSpace")
            {
                StringFinal = tokendots[0];
            }


            while (tokendots[0].IndexOf("  ") != -1)
            {
                tokendots[0] = tokendots[0].Replace("  ", " ");
            }

            string[] chartokens = tokendots[0].Split(new string[] { " " }, StringSplitOptions.None);

            if (needle == "OneSpace")
            {
                StringFinal = tokendots[0];
            }

            if (needle == "Standard")
            {
                foreach (string index in chartokens)
                {
                    string firstchar = index.Substring(0, 1);
                    firstchar = firstchar.ToUpper();
                    string temp = index.Remove(0, 1);
                    temp = temp.ToLower();

                    StringFinal += firstchar + temp + " ";
                }
            }

            for (int i = 0; i < tokens.Length - 1; ++i)
            {
                result += tokens[i] + "\\";
            }

            result += StringFinal.Trim() + "." + extensions;

            return result;

        }

        public StringArgs Args { get; set; }

        public StringAction Clone()
        {
            return new FullnameNormalizeAction()
            {
                Args = new FullnameNormalizeArgs()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new FullnameNormalizeDialog(Args as FullnameNormalizeArgs);

            if (screen.ShowDialog() == true)
            {
                var myArgs = Args as FullnameNormalizeArgs;
                myArgs.Needle = screen.Needle;
            }
        }
    }
}
