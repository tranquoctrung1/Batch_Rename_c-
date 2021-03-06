﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Batch_Rename
{
    public class MoveArgs : StringArgs, INotifyPropertyChanged
    {
        private string _needle;

        public string Details => $"Move on {Needle}";

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
            var myArgs = Args as MoveArgs;
            var needle = myArgs.Needle;

            string[] tokens = origin.Split(new string[] { "\\" }, StringSplitOptions.None);
            string[] tokendots = tokens[tokens.Length - 1].Split(new string[] { "." }, StringSplitOptions.None);
            string extensions = tokendots[tokendots.Length - 1];

            string[] StringChar = tokendots[0].Split(new string[] { " " }, StringSplitOptions.None);

            string temp = "  ";
            string StringFinal = null;
            string result = null;

            foreach (string index in StringChar)
            {
                if (index.Length == 13)
                {

                    char firstchar = index[0];
                    if (firstchar >= '0' && firstchar <= '9')
                    {
                        temp = index;
                    }
                }
            }

            while (tokendots[0].IndexOf(temp) != -1)
            {
                tokendots[0] = tokendots[0].Replace(temp, "");
            }

            StringFinal = tokendots[0];

            if (needle == "Head")
            {
                StringFinal = temp + " " + tokendots[0];
            }
            if (needle == "Tail")
            {
                StringFinal = tokendots[0] + " " + temp;
            }

            for (int i = 0; i < tokens.Length - 1; i++)
            {
                result += tokens[i] + "\\";
            }

            if(tokendots.Length < 2)
            {
                extensions = " ";
            }

            result += StringFinal + "." + extensions;

            while(result.IndexOf("  ") != -1)
            {
                result = result.Replace("  ", " ");
            }

            return result.Trim();
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
