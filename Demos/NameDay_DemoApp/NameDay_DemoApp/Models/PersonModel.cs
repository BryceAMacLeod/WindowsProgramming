using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NameDay_DemoApp.Models
{
    public class PersonModel
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string FullName { get; }
        public string Email { get; }
        public string Initials { get;  }

        public PersonModel (string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            FullName = firstName + " " + lastName;

            //https://stackoverflow.com/questions/10820273/regex-to-extract-initials-from-name
            Regex initials = new Regex(@"(\b[a-zA-Z])[a-zA-Z]* ?");
            Initials = initials.Replace(FullName, "$1");
        }

        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
