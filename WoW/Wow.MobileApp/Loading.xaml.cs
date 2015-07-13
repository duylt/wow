using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading;
using System.Windows.Threading;

namespace Wow.MobileApp
{
    public partial class Loading : PhoneApplicationPage
    {
        private DispatcherTimer dispatcherTimer;
        public Loading()
        {
            InitializeComponent();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        void Loading_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //do whatever you want to do here
            if (App.MyViewModel.IsLoadCompleted)
            {

                dispatcherTimer.Stop();
                this.NavigationService.Navigate(new Uri("/WebPage.xaml", UriKind.RelativeOrAbsolute));
                //if (!App.MyViewModel.MyChallenge.IsCompleted)
              
                //    this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
                //else
                //    this.NavigationService.Navigate(new Uri("/RemindPage.xaml", UriKind.RelativeOrAbsolute));

            }

        }
    }
}