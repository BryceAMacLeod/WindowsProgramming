using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNote.Models
{
    public class NoteModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public NoteModel(string Title, string Content)
        {
            this.Title = Title;
            this.Content = Content;
        }
    }
}
