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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorOrWhatever
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        decimal? numberOne;
        string nextOperation = "";

        public MainWindow()
        {
            InitializeComponent();
        }
        

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            result.Text += clickedButton.Content;
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            if (numberOne == null)
            {
                numberOne = decimal.Parse(result.Text);
                result.Text = "";
                nextOperation = "";
                nextOperation += clickedButton.Content;
            }

            else
            {
                switch (clickedButton.Content)
                {
                    case "+":
                        numberOne += decimal.Parse(result.Text);
                        break;
                    case "-":
                        numberOne -= decimal.Parse(result.Text);
                        break;
                    case "*":
                        numberOne *= decimal.Parse(result.Text);
                        break;
                    case "/":
                        if (result.Text == "0" || result.Text == "-" || result.Text == "-." || result.Text == "")
                        {
                            result.Text = "0";
                            numberOne = null;
                        }
                        else
                        {
                            numberOne /= decimal.Parse(result.Text);
                        }
                        break;
                }
                result.Text = "";
                nextOperation = "";
                nextOperation += clickedButton.Content;
            }
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (result.Text != "-" && result.Text != "-." && result.Text != "")
            { 
                switch (nextOperation)
                {
                    case "+":
                        result.Text = (numberOne + decimal.Parse(result.Text)).ToString();
                        break;
                    case "-":
                        result.Text = (numberOne - decimal.Parse(result.Text)).ToString();
                        break;
                    case "*":
                        result.Text = (numberOne * decimal.Parse(result.Text)).ToString();
                        break;
                    case "/":
                        if (result.Text == "0")
                        {
                            result.Text = "0";
                        }
                        else
                        {
                            result.Text = (numberOne / decimal.Parse(result.Text)).ToString();
                        }
                        break;
                    default:
                        break;
                }
            }
            
            nextOperation = "";
            numberOne = null;
        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            if (result.Text == "" || result.Text == ".")
            {
                result.Text = result.Text.Insert(0, "-");
            }
            else if (result.Text == "-")
            {
                result.Text = "";
            }
            else if (result.Text == "-.")
            {
                result.Text = ".";
            }
            else
            {
                result.Text = (-(decimal.Parse(result.Text))).ToString();
            }
        }

        private void Decimal_click(object sender, RoutedEventArgs e)
        {
            if (!result.Text.Contains("."))
            {
                result.Text += ".";
            }
        }
        private void ClearEntry_Click(object sender, RoutedEventArgs e)
        {
            numberOne = null;
            result.Text = "";
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            result.Text = "";
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (result.Text.Length > 0)
            {
                result.Text = result.Text.Remove(result.Text.Length - 1);
            }
        }
    }
}
