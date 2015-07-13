using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Wow.MobileApp
{
    public partial class WebPage : PhoneApplicationPage
    {
        public WebPage()
        {
            InitializeComponent();
            this.Loaded += WebPage_Loaded;
        }

        void WebPage_Loaded(object sender, RoutedEventArgs e)
        {
              
            this.webView.Navigate(new Uri("http://wakeorwaste.azurewebsites.net/Home/WakeOrWaste"));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.RemoveBackEntry();
            }
        }
    }
}