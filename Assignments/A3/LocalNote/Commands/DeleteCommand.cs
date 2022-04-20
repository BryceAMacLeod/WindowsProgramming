using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using LocalNote.Repositories;
using LocalNote.Models;
using Windows.UI.Xaml.Controls;
using System.Windows.Input;

namespace LocalNote.Commands
{
    public class DeleteCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ViewModels.LocalNoteViewModel lnvm;

        private List<NoteModel> _allNotes = new List<NoteModel>();

        public event EventHandler OnNoteDeleted;

        public DeleteCommand(ViewModels.LocalNoteViewModel noteViewModel, List<NoteModel> allNotes)
        {
            this.lnvm = noteViewModel;
            this._allNotes = allNotes;
        }

        public bool CanExecute(object parameter)
        {
            return lnvm.SelectedNote != null;
        }

        public async void Execute(object parameter)
        {
            DeleteNoteDialog dnd = new DeleteNoteDialog();
            ContentDialogResult result = await dnd.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                try
                {
                    LocalNoteRepo.DeleteNoteFromFile(lnvm.SelectedNote);

                    // Confirm with the user the note was deleted
                    ContentDialog successfulDeleteDialog = new ContentDialog()
                    {
                        Title = "Success",
                        Content = "Note deleted successfully, Hurray!",
                        PrimaryButtonText = "OK"
                    };

                    await successfulDeleteDialog.ShowAsync();

                    // Remove the note from the list
                    _allNotes.Remove(lnvm.SelectedNote);

                    // Trigger the deleted note event
                    OnNoteDeleted?.Invoke(this, new EventArgs());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Could not delete this note:\n" + ex);
                }
            }
        }

        public void FireCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
