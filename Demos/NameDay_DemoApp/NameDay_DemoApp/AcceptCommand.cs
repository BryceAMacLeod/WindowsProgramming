using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace NameDay_DemoApp
{
    public class AcceptCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private ViewModels.NameDaysViewModel ndvm;

        public AcceptCommand(ViewModels.NameDaysViewModel ndvm)
        {
            this.ndvm = ndvm;
        }

        public bool CanExecute(object parameter)
        {
            return ndvm.SelectedNameDay != null;
        }

        public async void Execute(object parameter)
        {
            //Debug.WriteLine(ndvm.SelectedNameDay.FullDate + 
            //    " - This nameday was just saved!");

            SaveNameDialog snd = new SaveNameDialog();
            ContentDialogResult result = await snd.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                //Save names here
                try
                {
                    Repositories.NameDaysRepo.SaveNamedaysToFile(ndvm.SelectedNameDay, snd.UserNote);

                    ContentDialog savedDialog = new ContentDialog() { Title = "Save Successful",
                        Content = "Names saved successfully to file, hurray!",
                        PrimaryButtonText = "OK" };
                    await savedDialog.ShowAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error when saving to file");
                }
            }
        }

        public void FireCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
