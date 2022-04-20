using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LocalNote.Models;
using LocalNote.ViewModels;
using LocalNote.Repositories;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace UnitTestProject_LocalNote
{
    [TestClass]
    public class UnitTesting
    {
        private LocalNoteViewModel lnvm = new LocalNoteViewModel();

        [TestMethod]
        public void CreateNewNote()
        {
            string title = "Create Note Test 2";
            string content = "Testing note content";

            NoteModel testNote = new NoteModel(title, content);

            //LocalNoteRepo.SaveNoteToFile(testNote);
            // waiting for the async AppendTextAsync method to finish as it blocks the assignment of LocalNoteRepo.lastSaved
            //Thread.Sleep(100);
            LocalNoteRepo.SaveNoteToDB(testNote);
            
            Assert.AreEqual(testNote.Title, LocalNoteRepo.LastSaved.Title);
            Assert.AreEqual(testNote.Content, LocalNoteRepo.LastSaved.Content);
        }

        [TestMethod]
        public void DeleteNote()
        {
            string title = "Delete Note Test";
            string content = "Testing note content";

            NoteModel testNote = new NoteModel(title, content);

            //LocalNoteRepo.SaveNoteToFile(testNote);
            //LocalNoteRepo.DeleteNoteFromFile(testNote);
            LocalNoteRepo.SaveNoteToDB(testNote);
            LocalNoteRepo.DeleteNoteFromDB(testNote);

            Assert.AreEqual(testNote.Title, LocalNoteRepo.LastDeleted.Title);
            Assert.AreEqual(testNote.Content, LocalNoteRepo.LastDeleted.Content);
        }

        [TestMethod]
        public void GetNotes()
        {
            string title = "Create Note for GetNotes Test";
            string content = "Testing note content";

            NoteModel testNote = new NoteModel(title, content);

            //LocalNoteRepo.SaveNoteToFile(testNote);
            LocalNoteRepo.SaveNoteToDB(testNote);

            //LocalNoteRepo.GetNotesFromFolder(lnvm._allNotes);
            LocalNoteRepo.GetNotesFromDB(lnvm._allNotes);
            
            NoteModel resultNote = lnvm._allNotes.Find(x => x.Title == "Create Note for GetNotes Test");
            Debug.WriteLine(resultNote.Title);
            Assert.AreEqual(resultNote.Title, testNote.Title);
            Assert.AreEqual(resultNote.Content, testNote.Content);
        }

    }
}
