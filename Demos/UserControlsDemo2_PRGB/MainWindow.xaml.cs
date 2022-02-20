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

namespace UserControlsDemo2_PRGB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IncrUserControl.IncrementMe += new EventHandler(IncrUserControl_Incremented);
            SecondIncrUserControl.IncrementMe += new EventHandler(SecondIncrUserControl_IncrementMe);
            //SecondIncrUserControl.IncrementMe += new EventHandler(IncrUserControl_Incremented);   //Or call the same event handler!
        }

        private void ShowLoginButton_Click(object sender, RoutedEventArgs e)
        {
            //for (int i = 0; i < 2; i++)
            //{
                Login myLogin = new Login();
                MainGrid.Children.Add(myLogin);

                Grid.SetColumn(myLogin, 0);
                Grid.SetRow(myLogin, 1);
            //}
        }

        private void IncrUserControl_Incremented(object sender, EventArgs e)
        {
            int x = int.Parse(DisplayBox.Text);
            DisplayBox.Text = (x + 1).ToString();
        }

        private void SecondIncrUserControl_IncrementMe(object? sender, EventArgs e)
        {
            int x = int.Parse(DisplayBox.Text);
            DisplayBox.Text = (x + 2).ToString();
        }
    }
}
