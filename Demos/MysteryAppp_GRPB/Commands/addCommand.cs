using MysteryAppp_GRPB.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using MysteryAppp_GRPB.Models;

namespace MysteryAppp_GRPB.Commands
{
    public class addCommand : ICommand
    {
        public GameCharacter gameCharacter { get; set; }

        public event EventHandler CanExecuteChanged;

        public event EventHandler OnCharacterCreated;
        public delegate void CharCreatedHandler(object sender, EventArgs e);

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            //Pop up a dialog to gather character information.
            AddNewCharacterDialog addNewCharacterDialog = new AddNewCharacterDialog();
            ContentDialogResult result = await addNewCharacterDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                //Instantiate as an object, save to collection.
                //string Name = addNewCharacterDialog.CharName;
                //string Desc = addNewCharacterDialog.Desc;
                //int level = addNewCharacterDialog.Level;

                gameCharacter = addNewCharacterDialog.TheCharacter;
                OnCharacterCreated?.Invoke(this, new EventArgs());


                //string result2 = addNewCharacterDialog.GeoffMethod(4);
            }
        }
    }
}
