using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LocalNote.Models;
using LocalNote.ViewModels;
using LocalNote.Repositories;
using System.Collections.Generic;
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

            LocalNoteRepo.SaveNoteToFile(testNote);
            // waiting for the async AppendTextAsync method to finish as it blocks the assignment of LocalNoteRepo.lastSaved
            Thread.Sleep(100);

            Assert.AreEqual(testNote.Title, LocalNoteRepo.lastSaved.Title);
            Assert.AreEqual(testNote.Content, LocalNoteRepo.lastSaved.Content);
        }

        [TestMethod]
        public void DeleteNote()
        {
            string title = "Delete Note Test";
            string content = "Testing note content";

            NoteModel testNote = new NoteModel(title, content);

            LocalNoteRepo.SaveNoteToFile(testNote);
            LocalNoteRepo.DeleteNoteFromFile(testNote);


            Assert.AreEqual(testNote.Title, LocalNoteRepo.lastDeleted.Title);
            Assert.AreEqual(testNote.Content, LocalNoteRepo.lastDeleted.Content);
        }

        [TestMethod]
        public void GetNotes()
        {
            string title = "Create Note for GetNotes Test";
            string content = "Testing note content";

            NoteModel testNote = new NoteModel(title, content);

            LocalNoteRepo.SaveNoteToFile(testNote);
            

            LocalNoteRepo.GetNotesFromFolder(lnvm._allNotes);

            NoteModel resultNote = lnvm._allNotes.Find(x => x.Title == "Create Note for GetNotes Test");
            Assert.AreEqual(resultNote.Title, testNote.Title);
            Assert.AreEqual(resultNote.Content, testNote.Content);
        }

    }
}
