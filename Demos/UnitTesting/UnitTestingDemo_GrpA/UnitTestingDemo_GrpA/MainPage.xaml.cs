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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UnitTestingDemo_GrpA
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MathModule mm = new MathModule();

        public MainPage()
        {
            this.InitializeComponent();
            DivByZero();
        }

        public void DoTheAdd()
        {
            //Pretend event handler hooked up to the add button onscreen
            //Assume the UI handles all data validation
            MathModule math = new MathModule();
            double result = mm.Add(6, 7);
        }

        public void DivByZero()
        {
            double result = mm.Divide(10, 0);
        }
    }
}
