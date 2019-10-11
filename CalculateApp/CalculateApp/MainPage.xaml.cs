using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalculateApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        List<string> input;
        bool calculated;
        public MainPage()
        {
            InitializeComponent();

            SavedFormula.Text = "";
            input = new List<string>();
            input.Add("0");
            calculated = false;
            showInput();
        }

        private void showInput()
        {
            Result.Text = "";
            foreach(string item in input)
            {
                Result.Text += item;
            }
        }

        private void addNumber(string num)
        {
            if (calculated)
            {
                calculated = false;
                input.Clear();
                input.Add("0");
            }
            if (input.Count == 1 && input[0] == "0")
            {
                input[input.Count - 1] = num;
            }
            else
            {
                input[input.Count - 1] += num;
            }

            showInput();
        }

        private void addOperation(string op)
        {
            if (input[0] == "Undefined")
            {
                input[0] = "0";
            }

            calculated = false;

            if (input[input.Count - 1] == ".")
            {
                input[input.Count - 1] = "0" + input[input.Count - 1];
            }
            if (input.Count > 1)
            {
                if (input[input.Count - 2].IndexOfAny(new char[] { '*', '/', '+', '-' }) != -1
                    && input[input.Count - 1] == "")
                {
                    input[input.Count - 2] = op;
                }
                else
                {
                    input.Add(op);
                    input.Add("");
                }
            }
            else
            {
                input.Add(op);
                input.Add("");
            }
            showInput();
        }

        private void btn7_Clicked(object sender, EventArgs e)
        {
            addNumber("7");
        }

        private void btn8_Clicked(object sender, EventArgs e)
        {
            addNumber("8");
        }

        private void btn9_Clicked(object sender, EventArgs e)
        {
            addNumber("9");
        }

        private void btnDivide_Clicked(object sender, EventArgs e)
        {
            addOperation("/");
        }

        private void btn4_Clicked(object sender, EventArgs e)
        {
            addNumber("4");
        }

        private void btn5_Clicked(object sender, EventArgs e)
        {
            addNumber("5");
        }

        private void btn6_Clicked(object sender, EventArgs e)
        {
            addNumber("6");
        }

        private void btnMulti_Clicked(object sender, EventArgs e)
        {
            addOperation("*");
        }

        private void btn1_Clicked(object sender, EventArgs e)
        {
            addNumber("1");
        }

        private void btn2_Clicked(object sender, EventArgs e)
        {
            addNumber("2");
        }

        private void btn3_Clicked(object sender, EventArgs e)
        {
            addNumber("3");
        }

        private void btnSub_Clicked(object sender, EventArgs e)
        {
            addOperation("-");
        }

        private void btn0_Clicked(object sender, EventArgs e)
        {
            addNumber("0");
        }

        private void btnDec_Clicked(object sender, EventArgs e)
        {
            if (input[input.Count - 1].IndexOf('.') == -1)
            {
                addNumber(".");
            }
        }

        private void btnAdd_Clicked(object sender, EventArgs e)
        {
            addOperation("+");
        }

        private void btnEqual_Clicked(object sender, EventArgs e)
        {
            calculated = true;

            if (input[input.Count - 1].IndexOfAny(new char[] { '*', '/', '+', '-',}) != -1 || input[input.Count - 1] == ".")
            {
                input.RemoveAt(input.Count - 1);
            }

            SavedFormula.Text = "";
            for(int i = 0; i < input.Count; i++)
            {
                SavedFormula.Text += input[i];
            }

            for (int i = 0; i < input.Count; i++)
            {
                if(input[i] == "*")
                {
                    double result = Convert.ToDouble(input[i - 1], CultureInfo.InvariantCulture) * Convert.ToDouble(input[i + 1], CultureInfo.InvariantCulture);
                    input[i] = result.ToString();
                    input.RemoveAt(i + 1);
                    input.RemoveAt(i - 1);
                    i = i-2;
                }
                else if(input[i] == "/")
                {
                    if(Convert.ToDouble(input[i - 1], CultureInfo.InvariantCulture) == 0.0)
                    {
                        input.Clear();
                        input.Add("Undefined");
                    }
                    else
                    {
                        double result = Convert.ToDouble(input[i - 1], CultureInfo.InvariantCulture) / Convert.ToDouble(input[i + 1], CultureInfo.InvariantCulture);
                        input[i] = result.ToString();
                        input.RemoveAt(i + 1);
                        input.RemoveAt(i - 1);
                        i = i - 2;
                    }
                }
            }

            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] == "+")
                {
                    double result = Convert.ToDouble(input[i - 1], CultureInfo.InvariantCulture) + Convert.ToDouble(input[i + 1], CultureInfo.InvariantCulture);
                    input[i] = result.ToString();
                    input.RemoveAt(i + 1);
                    input.RemoveAt(i - 1);
                    i = i - 2;
                }
                else if (input[i] == "-")
                {
                    double result = Convert.ToDouble(input[i - 1], CultureInfo.InvariantCulture) - Convert.ToDouble(input[i + 1], CultureInfo.InvariantCulture);
                    input[i] = result.ToString();
                    input.RemoveAt(i + 1);
                    input.RemoveAt(i - 1);
                    i = i - 2;
                }
            }

            showInput();
        }
    }
}
