using Facebook;
using FaceBookWp8._1.Helpers;
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
    public sealed partial class Login : Page, IWebAuthenticationBrokerContinuable
    {
        readonly Uri _loginUrl;
        public Login()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
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
        }
        private void BtnFaceBookLogin_Click(object sender, RoutedEventArgs e)
        {
            App.ObjFBHelper.LoginAndContinue();
        }
        FacebookClient fbclient = new FacebookClient();
        public async void ContinueWithWebAuthenticationBroker(WebAuthenticationBrokerContinuationEventArgs args)
        {
            App.ObjFBHelper.ContinueAuthentication(args);
            if (App.ObjFBHelper.AccessToken != null)
            {
                fbclient = new Facebook.FacebookClient(App.ObjFBHelper.AccessToken);

                //Fetch facebook UserProfile:
                dynamic result = await fbclient.GetTaskAsync("me");
                string id = result.id;
                string email = result.email;
                string FBName = result.name;

                //Format UserProfile:
                GetUserProfilePicture(id);
                TxtUserProfile.Text = FBName;
                ProfileHelpers.ProfileName = FBName;
                ProfileHelpers.IsLogin = true;

                // Login finish
                // Sync infomation
                var challenge = App.MyWakeOrWasteBL.GetMyChallenge();
              

                //StckPnlProfile_Layout.Visibility = Visibility.Visible;
                //BtnLogin.Visibility = Visibility.Collapsed;
                //BtnLogout.Visibility = Visibility.Visible;

                Frame.GoBack();
            }
            else
            {
                // StckPnlProfile_Layout.Visibility = Visibility.Collapsed;
            }

        }
        private void GetUserProfilePicture(string UserID)
        {
            string profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", UserID, "square", App.ObjFBHelper.AccessToken);

            ProfileHelpers.ProfileImage = profilePictureUrl;
            picProfile.Source = new BitmapImage(new Uri(profilePictureUrl));
        }

        Uri _logoutUrl;
        //private async void BtnFaceBookLogout_Click(object sender, RoutedEventArgs e)
        //{
        //    _logoutUrl = fbclient.GetLogoutUrl(new
        //    {
        //        next = "https://www.facebook.com/connect/login_success.html",
        //        access_token = App.ObjFBHelper.AccessToken
        //    });
        //    WebAuthenticationBroker.AuthenticateAndContinue(_logoutUrl);
        //    BtnLogin.Visibility = Visibility.Visible;
        //    BtnLogout.Visibility = Visibility.Collapsed;
        //}
    }
}
