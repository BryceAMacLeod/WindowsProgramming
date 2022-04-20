using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace LocalNote.Commands
{
    public class AboutCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            ContentDialog AboutApplicationDialog = new ContentDialog();
         
            AboutApplicationDialog.Title = "About this App";
            AboutApplicationDialog.Content = "Locally sourced storage and management of your notes";
            AboutApplicationDialog.PrimaryButtonText = "OK";
            
            await AboutApplicationDialog.ShowAsync();
        }
    }
}
