using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using LocalNote.Models;
using Microsoft.Data.Sqlite;

namespace LocalNote.Repositories
{
    public class LocalNoteRepo
    {
        public static StorageFolder notesFolder = ApplicationData.Current.LocalFolder;
        // full path to db
        public static string dbpath = Path.Combine(notesFolder.Path, "LocalNote.db");
        // For unit testing purposes
        public static NoteModel LastSaved { get; set; }
        public static NoteModel LastDeleted { get; set; }
        public static void GetNotesFromFolder(List<NoteModel> AllNotes)
        {
            try
            {
                var noteFiles = Directory.GetFiles(notesFolder.Path, "*.txt");
                foreach (var noteFile in noteFiles)
                {
                    // getting title of note
                    var Title = Path.GetFileName(noteFile);
                    // removing .txt extension
                    Title = Title.Remove(Title.Length - 4);
                    // getting content of note
                    string Content = File.ReadAllText(noteFile);
                    NoteModel note = new NoteModel(Title, Content);
                    AllNotes.Add(note);
                }
            } 
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            
        }
        public async static void SaveNoteToFile(NoteModel Selected)
        {
            String fileName = Selected.Title + ".txt";
            
            try
            {
                
                StorageFile noteFile = await notesFolder.CreateFileAsync(fileName,
                    CreationCollisionOption.ReplaceExisting);
                await FileIO.AppendTextAsync(noteFile, Selected.Content);

                LastSaved = Selected;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }
        public static void DeleteNoteFromFile(NoteModel Selected)
        {
            String fileName = Selected.Title + ".txt";

            try
            {
                if (File.Exists(notesFolder.Path + '/' + fileName))
                {
                    File.Delete(notesFolder.Path + '/' + fileName);
                    LastDeleted = Selected;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error deleting file: " + ex.Message);
            }
        }
        public static async void InitializeDatabase()
        {
            // create the db file in windows storage
            await notesFolder.CreateFileAsync("LocalNote.db", CreationCollisionOption.OpenIfExists);
            
            // open connection to db
            using (SqliteConnection conn = new SqliteConnection($"Filename={dbpath}"))
            {
                conn.Open();
                // define create table script
                string tableCommand = "CREATE TABLE IF NOT EXISTS NoteTable " + 
                    "(Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "Title TEXT NOT NULL UNIQUE, " +
                    "Content TEXT NOT NULL);";
                // command object (running the string)
                SqliteCommand cmd = new SqliteCommand(tableCommand, conn); 
                try
                {
                    // execute the sql command and close the DB connection
                    cmd.ExecuteReader();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("An error occured when initializing the DB: " + ex.Message);
                }
            }
        }
        public static void GetNotesFromDB(List<NoteModel> AllNotes)
        {
            using(SqliteConnection conn = new SqliteConnection($"Filename={dbpath}"))
            {
                conn.Open();

                SqliteCommand getCommand = new SqliteCommand("SELECT Title, Content FROM NoteTable", conn);
                try
                {
                    SqliteDataReader reader = getCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        // create note from db entry
                        NoteModel note = new NoteModel(reader.GetString(0), reader.GetString(1));
                        AllNotes.Add(note);
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("An error occured while reading from database: " + ex.Message);
                }
                finally
                {
                    conn?.Close();
                }
            }
        }
        public static void SaveNoteToDB(NoteModel Selected)
        {
            using (SqliteConnection conn = new SqliteConnection($"Filename={dbpath}"))
            {
                conn.Open();
                SqliteCommand saveCommand = new SqliteCommand();
                saveCommand.Connection = conn;
                saveCommand.CommandText = "INSERT INTO NoteTable VALUES (Null, @Title, @Content);";
                saveCommand.Parameters.AddWithValue("@Title", Selected.Title);
                saveCommand.Parameters.AddWithValue("@Content", Selected.Content);
                try
                {
                    // Insert entry to db
                    saveCommand.ExecuteReader();
                    LastSaved = Selected;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("An error occured while inserting into the database: " + ex.Message);
                }
                finally
                {
                    conn?.Close();
                }
            }
        }
        public static void DeleteNoteFromDB(NoteModel Selected)
        {
            using (SqliteConnection conn = new SqliteConnection($"Filename={dbpath}"))
            {
                conn.Open();
                SqliteCommand deleteCommand = new SqliteCommand();
                deleteCommand.Connection = conn;
                deleteCommand.CommandText = "DELETE FROM NoteTable WHERE Title = @Title;";
                deleteCommand.Parameters.AddWithValue("@Title", Selected.Title);
                try
                {
                    // delete note from DB
                    deleteCommand.ExecuteReader();
                    LastDeleted = Selected;
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("An error occured while deleting entry from database: " + ex.Message);
                }
                finally
                {
                    conn?.Close();
                }
            }
        }
    }
}
