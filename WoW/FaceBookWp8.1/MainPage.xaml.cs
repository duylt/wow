using Facebook;
using FaceBookWp8._1.Helpers;
using FaceBookWp8._1.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace FaceBookWp8._1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.Loaded += MainPage_Loaded;

        }
        private WakeWasteViewModel _viewModel;
        public WakeWasteViewModel MyViewModel
        {
            get
            {
                if(_viewModel == null)
                {
                    _viewModel = new WakeWasteViewModel();
                    _viewModel.Init();
                   
                }
                return _viewModel;
            }
        }


        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!ProfileHelpers.IsLogin)
            {
                // go to login page
                Frame.Navigate(typeof(Login));
            }
            //  Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync();
            this.DataContext = MyViewModel;

        }


        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
            base.OnNavigatedTo(e);

            if (ProfileHelpers.IsLogin)
            {
                this.ProfileImage.Source = new BitmapImage(new Uri(ProfileHelpers.ProfileImage));
                // this.ProfileName.Text = ProfileHelpers.ProfileName;
            }
            else
            {
                Frame.Navigate(typeof(Login));
            }

        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }
        Uri _logoutUrl;
        private void ProfileImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FacebookClient fbclient = new FacebookClient();
            _logoutUrl = fbclient.GetLogoutUrl(new
              {
                  next = "https://www.facebook.com/connect/login_success.html",
                  access_token = App.ObjFBHelper.AccessToken,
                //  redirect_uri =  App.ObjFBHelper._callbackUri.AbsoluteUri,
              });
            ProfileHelpers.IsLogin = false;
            ProfileHelpers.ProfileImage = "";
            WebAuthenticationBroker.AuthenticateAndContinue(_logoutUrl);
            

           // Frame.Navigate(typeof(Login));
            //BtnLogin.Visibility = Visibility.Visible;
            //BtnLogout.Visibility = Visibility.Collapsed;

        }

    }
}
