using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LocalNote
{
    /// <summary>
    /// An About page that displays Application and Developer information
    /// </summary>
    public sealed partial class About : Page
    {
        public About()
        {
            this.InitializeComponent();

            Package package = Package.Current;
            PackageId id = package.Id;
            PackageVersion version = id.Version;
            string appName = package.DisplayName;
            string publisher = package.PublisherDisplayName;

            appInfo.Text = string.Format("Name: " + appName + "\nPublisher: " + publisher +
                "\nVersion: {0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
            base.OnNavigatedTo(e);
        }
        private void OnBackRequested(object sender, BackRequestedEventArgs backRequestedEventArgs)
        {
            if(Frame.CanGoBack)
            {
                Frame.GoBack();
            }
            backRequestedEventArgs.Handled = true;
        }
    }
}
