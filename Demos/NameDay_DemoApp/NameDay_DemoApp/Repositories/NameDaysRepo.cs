using NameDay_DemoApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NameDay_DemoApp.Repositories
{
    public class NameDaysRepo
    {
        private static StorageFolder namesFolder = ApplicationData.Current.LocalFolder;

        public async static void SaveNamedaysToFile(NameDayModel selected, String userNote)
        {
            //Build a file name
            String fileName = selected.MonthName + selected.Day.ToString() + ".txt";
            try
            {
                StorageFile nameDayFile = await namesFolder.CreateFileAsync(fileName,
                    CreationCollisionOption.OpenIfExists);
                await Windows.Storage.FileIO.AppendTextAsync(nameDayFile, "Today's names: "
                    + selected.NamesAsString + " NOTES: " + userNote);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Oh noes! An error occurred with file writing. Ahhhhh!");
            }
        }
    }
}
