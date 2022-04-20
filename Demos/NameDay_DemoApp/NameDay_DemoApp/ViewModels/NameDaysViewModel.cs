using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NameDay_DemoApp.Models;

namespace NameDay_DemoApp.ViewModels
{
    public class NameDaysViewModel : INotifyPropertyChanged
    {
        public AcceptCommand AcceptCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<NameDayModel> NameDays { get; set; }
        public ObservableCollection<PersonModel> Celebrants { get; set; }
        private List<NameDayModel> _allNamedays = new List<NameDayModel>();

        private NameDayModel _selectedNameday;
        public string DayDescription { get; set; }

        private string _filter;

        public NameDaysViewModel ()
        {
            AcceptCommand = new AcceptCommand(this);

            //Hard-code some dummy data to start thing off
            NameDays = new ObservableCollection<NameDayModel>();

            for (int month = 1; month <= 12; month++)
            {
                NameDayModel first = new NameDayModel(month, 1, 
                    new ObservableCollection<string> { "David", "Billy"},
                    "This is David and Billy Day.",
                    new ObservableCollection<PersonModel>
                    { new PersonModel("David", "Smith", "david@namedays.ca") });

                NameDayModel second = new NameDayModel(month, 15,
                    new ObservableCollection<string> { "Mary" },
                    "This is Mary Day.",
                    new ObservableCollection<PersonModel>
                    { new PersonModel("Mary", "Johnson", "mary@namedays.ca"),
                      new PersonModel("Mary", "Brown", "maryb@namedays.ca") });

                _allNamedays.Add(first);
                _allNamedays.Add(second);
            }
            PerformFiltering();
        }

        public NameDayModel SelectedNameDay
        {
            get { return _selectedNameday; }
            set
            {
                _selectedNameday = value;
                if (value == null)
                {
                    DayDescription = "No description available for this day.";
                }
                else
                {
                    DayDescription = value.DayDesc;
                    Celebrants = value.People;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DayDescription"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Celebrants"));
                //Event to call the save functionality
                AcceptCommand.FireCanExecuteChanged();
            }
        }

        public string Filter
        {
            get { return _filter; }
            set
            {
                if (value == _filter) { return; }
                _filter = value;
                PerformFiltering();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filter)));
            }
        }

        private void PerformFiltering()
        {
            if (_filter == null)
            {
                _filter = "";
            }
            //If _filter has a value (ie. user entered something in Filter textbox)
            //Lower-case and trim string
            var lowerCaseFilter = Filter.ToLowerInvariant().Trim();

            //Use LINQ query to get all personmodel names that match filter text, as a list
            var result =
                _allNamedays.Where(d => d.NamesAsString.ToLowerInvariant()
                .Contains(lowerCaseFilter))
                .ToList();

            //Get list of values in current filtered list that we want to remove
            //(ie. don't meet new filter criteria)
            var toRemove = NameDays.Except(result).ToList();

            //Loop to remove items that fail filter
            foreach (var x in toRemove)
            {
                NameDays.Remove(x);
            }

            var resultCount = result.Count;
            // Add back in correct order.
            for (int i = 0; i < resultCount; i++)
            {
                var resultItem = result[i];
                if (i + 1 > NameDays.Count || !NameDays[i].Equals(resultItem))
                {
                    NameDays.Insert(i, resultItem);
                }
            }
        }
    }
}
