using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MysteryAppp_GRPB.Models;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MysteryAppp_GRPB.UserControls
{
    public sealed partial class AddNewCharacterDialog : ContentDialog
    {
        //public string CharName { get; set; }
        //public string Desc { get; set; }   
        //public int Level { get; set; }

        public GameCharacter TheCharacter;

        public AddNewCharacterDialog()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            TheCharacter = new GameCharacter(NameTextbox.Text, DescTextbox.Text, int.Parse(LevelTextbox.Text));
            //CharName = NameTextbox.Text;
            //Desc = DescTextbox.Text;
            //Level = int.Parse(LevelTextbox.Text);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        public string GeoffMethod(int myNum)
        {
            return "Hi;";
        }
    }
}
