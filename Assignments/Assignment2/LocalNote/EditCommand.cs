using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace LocalNote
{
    public class EditCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public event EventHandler OnNoteChanged;

        private ViewModels.LocalNoteViewModel lnvm;

        public EditCommand(ViewModels.LocalNoteViewModel lnvm)
        {
            this.lnvm = lnvm;
        }

        public bool CanExecute(object parameter)
        {
            return lnvm.SelectedNote != null;
        }

        public void Execute(object parameter)
        {
            try
            {
                MainPage.EditAppBarButton.IsEnabled = false;
                MainPage.EditContentTextbox.IsReadOnly = false;

                // trigger note change event
                OnNoteChanged?.Invoke(this, new EventArgs());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        public void FireCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
