using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameDay_DemoApp.Models
{
    public class NameDayModel //: IEnumerable
    {
        public int Month { get; }
        public int Day { get; }
        public ObservableCollection<string> AssociatedNames { get; }
        public string DayDesc { get; }
        public ObservableCollection<PersonModel> People { get; }

        public NameDayModel(int month, int day, 
            ObservableCollection<string> associatedNames,
            string dayDesc,
            ObservableCollection<PersonModel> people)
        {
            this.Month = month;
            this.Day = day;
            this.AssociatedNames = associatedNames;
            this.DayDesc = dayDesc;
            this.People = people;
        }

        public string NamesAsString => string.Join(", ", AssociatedNames);

        public string MonthName
        {
            get
            {
                return Enum.GetName(typeof(MonthEnum), Month);
            }
        }

        public string FullDate
        {
            get
            {
                return Enum.GetName(typeof(MonthEnum), Month) + " " + Day;
            }
        }
    }
}
