using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WoW.Biz.Social;
using WoW.Biz;

namespace WoW.Web.Controllers
{
    public class AccountController : Controller
    {
        //private IWoWBiz
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("SocialLoginback");
                return uriBuilder.Uri;
            }
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult SocialLogin()
        {
            FacebookConnector fb = new FacebookConnector();
            return this.Redirect(fb.LoginUrl(RedirectUri.AbsoluteUri));
            //return View();
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SocialLoginback(string code, string error, string error_reason)
        {
            var fb = new FacebookConnector();
            var wowBiz = new WoWBiz();

            var accessToken = fb.GetAccessToken(code, RedirectUri.AbsoluteUri);

            // Store the access token in the session for farther use
            Session["AccessToken"] = accessToken;

            // Get user profile from facebook
            dynamic userProfile = fb.GetUserProfile(accessToken);

            #region User Process
            var clientId = userProfile.id;
            var userProfileImage = string.Format("https://graph.facebook.com/{0}/picture", userProfile.id);
            //Create user for first times login
            var existingUser = wowBiz.GetUserByClientId((string)clientId);
            if (existingUser == null)
            {
                var user = new Core.User
                {
                    ImageUrl = userProfileImage,
                    Name = string.Format("{0} {1}", userProfile.first_name, userProfile.last_name),
                    Socials = new List<Core.Social>{
                        new Core.Social{
                            ClientId_ = clientId,
                            Email = userProfile.email,
                            Name = string.Format("{0} {1}", userProfile.first_name, userProfile.last_name),
                            Token = accessToken,
                            Type = (int)Core.Enum.SocialType.Facebook,
                        }
                    }
                };
                var success = wowBiz.CreateNewUser(user);
                existingUser = user;
            }
            else
            {
                //Update 
                wowBiz.UpdateSocialToken(existingUser.Socials.First().Id, accessToken);
            }
            Session["User"] = existingUser;
            #endregion
            // Set the auth cookie
            FormsAuthentication.SetAuthCookie(userProfile.email, false);
            return RedirectToAction("Index", "Home");
        }
    }
}
