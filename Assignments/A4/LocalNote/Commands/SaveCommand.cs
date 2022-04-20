using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using LocalNote.Models;
using LocalNote.Repositories;


namespace LocalNote.Commands
{
    public class SaveCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event EventHandler OnNoteCreated;

        public NoteModel NewNote { get; set; }

        private ViewModels.LocalNoteViewModel lnvm;

        private List<NoteModel> _allNotes = new List<NoteModel>();

        public SaveCommand(ViewModels.LocalNoteViewModel lnvm, List<NoteModel> AllNotes)
        {
            this.lnvm = lnvm;
            this._allNotes = AllNotes;
        }

        public bool CanExecute(object parameter)
        {
            if (lnvm.SelectedNote == null || MainPage.EditContentTextbox.IsReadOnly == false) {
                return true;
            }
            return false;
        }

        public async void Execute(object parameter)
        {
            SaveNoteDialog snd = new SaveNoteDialog();
            ContentDialogResult result = await snd.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                // Try saving note to file.
                try
                {
                    // add new note into list of notes
                    NewNote = new NoteModel(snd.NoteTitle, lnvm.Content);

                    bool nameExists = false;

                    foreach(var note in _allNotes)
                    {
                        if(note.Title == NewNote.Title)
                        {
                            nameExists = true;
                        }
                    }
                    if(!nameExists)
                    {
                        //LocalNoteRepo.SaveNoteToFile(newNote);
                        LocalNoteRepo.SaveNoteToDB(NewNote);

                        ContentDialog savedDialog = new ContentDialog()
                        {
                            Title = "Save Successful",
                            Content = "Note saved successfully to file/DB, hurray!",
                            PrimaryButtonText = "OK"
                        };
                        await savedDialog.ShowAsync();

                        // triggering new not event
                        OnNoteCreated?.Invoke(this, new EventArgs());
                    }
                    

                    
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error when saving to file: " + ex.Message);
                }
            }
        }

        public void FireCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
