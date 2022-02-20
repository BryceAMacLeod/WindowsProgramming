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

namespace EventsDemo_GrpB
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

        private void ClickMeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageTextBlock.Text = sender.ToString() + " was clicked!";
        }

        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
            MessageTextBlock.Text = "\nSTACKPANEL was clicked.";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageTextBlock.Text = "\nSecondButton was clicked.";
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            MessageTextBlock.Text = "\nGRID was clicked.";
        }

        private void ClickMeButton_MouseMove(object sender, MouseEventArgs e)
        {
            //MessageTextBlock.Text = "Mouse on: " + sender.ToString();
            double x = e.GetPosition(this).X;   //Current X position of the mouse cursor
            double y = e.GetPosition(this).Y;   //Current Y position of the mouse cursor

            MessageTextBlock.Text = "Current position: " + x + " : " + y;
        }

        private void ClickMeButton_MouseLeave(object sender, MouseEventArgs e)
        {
            MessageTextBlock.Text = String.Empty;
        }

        private void Ellipse_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
        }
    }
}
