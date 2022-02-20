using Microsoft.Win32;
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

namespace MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // holding the current file
        TagLib.File currentFile;
        public MainWindow()
        {
            
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            // instantiating an OpenFileDialog object
            OpenFileDialog fileDlg = new OpenFileDialog();

            // filtering to files with mp3 extensions
            fileDlg.Filter = "MP3 files (*.mp3)|*.mp3";

            //ShowDialog displays onscreen to user by invoking showDialog()
            if(fileDlg.ShowDialog() == true)
            {
                // the filename property stores the full path for the file selected
                fileNameBox.Text = fileDlg.FileName;

                // creating taglib file object for accessing the mp3 + tags
                currentFile = TagLib.File.Create(fileDlg.FileName);

                // source of media element to currentfile (uri)
                myMediaPlayer.Source = new Uri(fileDlg.FileName);
                myMediaPlayer.Play();
            }
        }

        private void TagButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
