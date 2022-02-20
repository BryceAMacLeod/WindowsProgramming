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

namespace CustomControlsDemo_GRPB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GeoffTestButton myButton;   //Showing how we could create our own button class (inherited), wrapped
        UserControl1 myCustomControl;

        public MainWindow()
        {
            InitializeComponent();
            myButton = new GeoffTestButton();
            myCustomControl = new UserControl1();  
        }

        private void ClickMeButton_Click(object sender, RoutedEventArgs e)
        {
            //myCustomControl.show();
        }
    }
}
