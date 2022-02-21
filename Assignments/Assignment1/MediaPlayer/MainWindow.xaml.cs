using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace MediaPlayer
{
    /// <summary>
    /// Assignment one - Mp3 player / metadata tag editor
    /// </summary>
    public partial class MainWindow : Window
    {
        // holding the current file
        TagLib.File? currentFile = null;

        TagEditor myTagEditor = new TagEditor();
        NowPlaying nowPlaying = new NowPlaying();
        
        bool taggingToggled, nowPlayingToggled = false;

        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;

        public MainWindow()
        {
            InitializeComponent();

        // timer to keep track of song progress
        // resource retrieved from 
        // http://www.wpf-tutorial.com/audio-video/how-to-creating-a-complete-audio-video-player/

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            myTagEditor.TagsChanged += new EventHandler(TagEditor_Save);
        }

        /// <summary>
        /// Handles the opening of the mp3 file
        /// </summary>
        private void OpenFile(object sender, RoutedEventArgs e)
        {
            // instantiating an OpenFileDialog object
            OpenFileDialog fileDlg = new OpenFileDialog();

            // filtering to files with mp3 extensions
            fileDlg.Filter = "MP3 files (*.mp3)|*.mp3";

            // if user opens an mp3 file
            if(fileDlg.ShowDialog() == true)
            {
                try
                {
                    // if user selects another file then close current
                    CloseFile(sender, e);
                    // creating taglib file object for accessing the mp3 + tags
                    currentFile = TagLib.File.Create(fileDlg.FileName);

                    // source of media element to currentfile
                    myMediaPlayer.Source = new Uri(fileDlg.FileName);
                }
                // basic error handling to prevent crashing
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
        }

        /// <summary>
        /// Handles the closing of mp3 files, and saving of meta data
        /// </summary>
        private void CloseFile(object sender, RoutedEventArgs e)
        {
            if (currentFile != null)
            {
                myMediaPlayer.Stop();
                myMediaPlayer.Close();
                myMediaPlayer.Source = null;
                try
                {
                    if (currentFile.Writeable)
                    {
                        currentFile.Save();
                    }

                    currentFile.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error saving metadata " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Handles the local storage of tag metadata
        /// and closes tag editor screen from GUI
        /// </summary>
        
        private void TagEditor_Save(object sender, EventArgs e)
        {
            if(currentFile != null)
            {
                if (sender is TagEditor myTagEditor)
                {
                    currentFile.Tag.Title = myTagEditor.SongTitle.Text;
                    currentFile.Tag.Performers[0] = myTagEditor.Artist.Text;
                    currentFile.Tag.Album = myTagEditor.Album.Text.ToString();
                    currentFile.Tag.Year = uint.Parse(myTagEditor.Year.Text);
                    theCanvas.Children.Remove(myTagEditor);
                    taggingToggled = false;
                }
            }
        }
        /// <summary>
        /// Handles the display of tag editor to the GUI
        /// </summary>
        private void TaggingButton_Click(object sender, RoutedEventArgs e)
        {
            if(taggingToggled)
            {
                theCanvas.Children.Remove(myTagEditor);
                taggingToggled = false;
                
            }
            else if(nowPlayingToggled)
            {
                theCanvas.Children.Remove(nowPlaying);
                nowPlayingToggled = false;
                TaggingButton_Click(sender, e);
            }
            else if(currentFile != null)
            {
                myTagEditor.SongTitle.Text = currentFile.Tag.Title;
                myTagEditor.Artist.Text = (currentFile.Tag.Performers.Length > 0) ? currentFile.Tag.Performers[0] : "";
                myTagEditor.Album.Text = currentFile.Tag.Album;
                myTagEditor.Year.Text = currentFile.Tag.Year.ToString();
                theCanvas.Children.Add(myTagEditor);
                taggingToggled = true;
            }
        }

        /// <summary>
        /// Handles the display of the Now Playing Metadata to the GUI
        /// </summary>
        private void NowPlayingButton_Click(object sender, RoutedEventArgs e)
        {
            if (nowPlayingToggled)
            {
                theCanvas.Children.Remove(nowPlaying);
                nowPlayingToggled = false;

            }
            else if (taggingToggled)
            {
                theCanvas.Children.Remove(myTagEditor);
                taggingToggled = false;
                NowPlayingButton_Click(sender, e);
            }
            else if (currentFile != null)
            {
                nowPlaying.SongTitle.Content = currentFile.Tag.Title;
                nowPlaying.Artist.Content = (currentFile.Tag.Performers.Length > 0) ? currentFile.Tag.Performers[0] : "";
                nowPlaying.Album.Content = currentFile.Tag.Album;
                nowPlaying.Year.Content = currentFile.Tag.Year.ToString();
                theCanvas.Children.Add(nowPlaying);
                nowPlaying.HorizontalAlignment = HorizontalAlignment.Right;
                nowPlaying.VerticalAlignment = VerticalAlignment.Bottom;

                nowPlayingToggled = true;
            }
        }

        /// <summary>
        /// Handles the song-timer synchronization
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            if ((myMediaPlayer.Source != null) && (myMediaPlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = myMediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = myMediaPlayer.Position.TotalSeconds;
            }
        }
        

        private void OpenCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFile(sender, e);
        }

        private void CloseCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (myMediaPlayer != null) && (myMediaPlayer.Source != null);
        }
        private void CloseCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CloseFile(sender, e);
        }

        private void PlayCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (myMediaPlayer != null) && (myMediaPlayer.Source != null);
        }
        private void PlayCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myMediaPlayer?.Play();
            mediaPlayerIsPlaying = true;
        }

        private void PauseCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            
            e.CanExecute = mediaPlayerIsPlaying;
            
        }
        private void PauseCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myMediaPlayer?.Pause();
        }

        private void StopCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
            
        }
        private void StopCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myMediaPlayer?.Stop();
            mediaPlayerIsPlaying = false;
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            myMediaPlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

    }
}
