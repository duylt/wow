using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Wow.MobileApp.Resources;
using System.Windows.Media.Imaging;

namespace Wow.MobileApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor

        public MainPage()
        {
            InitializeComponent();

            Loaded += MainPage_Loaded;

        }

        public ToggleWakeWasteViewModel PageViewModel { get; set; }


        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            PageViewModel = new ToggleWakeWasteViewModel();
            PageViewModel.Init();
            this.ProfileImage.Source = new BitmapImage(new Uri(App.MyViewModel.MyUserProfile.ProfileImageUrl));
            this.DataContext = PageViewModel;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.RemoveBackEntry();
            }
        }

        private void btnWakeWaste_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var webClient = new WebClient();
            webClient.DownloadStringCompleted += webClient_DownloadStringCompleted;
            string url = "";
            if (PageViewModel.IsWake())
            {
                url = string.Format("http://wakeorwaste.azurewebsites.net/api/public/ChallengeWake?challengeId={0}", PageViewModel.ChallengeId);
            }
            else
            {
                url = string.Format("http://wakeorwaste.azurewebsites.net/api/public/ChallengeWaste?challengeId={0}", PageViewModel.ChallengeId);
            }

            webClient.DownloadStringAsync(new Uri(url, UriKind.RelativeOrAbsolute));
        }

        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/RemindPage.xaml", UriKind.RelativeOrAbsolute));
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}