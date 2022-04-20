using MysteryAppp_GRPB.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryAppp_GRPB.ViewModels
{
    public class GameCharacterViewModel
    {
        public Commands.addCommand addCommand = new Commands.addCommand();

        public ObservableCollection<GameCharacter> MyParty = new ObservableCollection<GameCharacter>();

        public GameCharacterViewModel()
        {
            addCommand.OnCharacterCreated += AddCommand_OnCharacterCreated;
        }

        private void AddCommand_OnCharacterCreated(object sender, EventArgs e)
        {
            if (addCommand.gameCharacter != null)
            {
                MyParty.Add(addCommand.gameCharacter);
            }
        }
    }
}
