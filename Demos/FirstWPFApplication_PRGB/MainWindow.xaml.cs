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

namespace FirstWPFApplication_PRGB
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

        private void RollButton_Click(object sender, RoutedEventArgs e)
        {
            //Get 4 random numbers
            Random random = new Random();
            int firstNum = random.Next(1, 11);
            int secondNum = random.Next(1, 11);
            int thirdNum = random.Next(1, 11);
            int fourthNum = random.Next(1, 11);
            int fifthNum = random.Next(1, 11);

            //Assign them into the onscreen labels
            firstTextBlock.Text = firstNum.ToString();
            secondTextBlock.Text = secondNum.ToString();
            thirdTextBlock.Text = thirdNum.ToString();
            fourthTextBlock.Text = fourthNum.ToString();
            fifthTextBlock.Text = fifthNum.ToString();
        }

        private void MoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Grid.GetColumn(NumbersPanel) == 0 )
                {
                Grid.SetColumn(NumbersPanel, 1);
            }
            else
            {
                Grid.SetColumn(NumbersPanel, 0);
            }
        }
    }
}
