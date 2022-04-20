using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using LocalNote.Models;

namespace LocalNote.Repositories
{
    public class LocalNoteRepo
    {
        public static StorageFolder notesFolder = ApplicationData.Current.LocalFolder;
        
        public static void GetNotesFromFolder(List<NoteModel> allNotes)
        {
            try
            {
                var noteFiles = Directory.GetFiles(notesFolder.Path, "*.txt");
                foreach (var noteFile in noteFiles)
                {
                    // getting title of note
                    var Title = Path.GetFileName(noteFile);
                    // removing .txt extension
                    Title = Title.Remove(Title.Length - 5);
                    // getting content of note
                    string Content = File.ReadAllText(noteFile);
                    NoteModel file = new NoteModel(Title, Content);
                    allNotes.Add(file);
                }

               
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            
        }
        public async static void SaveNoteToFile(NoteModel selected)
        {
            String fileName = selected.Title + ".txt";
            
            try
            {
                StorageFile noteFile = await notesFolder.CreateFileAsync(fileName,
                    CreationCollisionOption.ReplaceExisting);
                await Windows.Storage.FileIO.AppendTextAsync(noteFile, selected.Content);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }
        
    }
}
