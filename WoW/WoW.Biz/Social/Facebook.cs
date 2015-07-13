using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WoW.Core;
using WoW.Core.Constract;

namespace WoW.Biz.Social
{
    public class FacebookConnector : SocialBase<FacebookConnector>, ISocial
    {
        #region Properties
        private string AppID
        {
            get
            {
                return Setting("AppId");
            }
        }
        private string AppSecret
        {
            get
            {
                return Setting("AppSecret");
            }
        }
        private string ClientToken
        {
            get
            {
                return Setting("ClientToken");
            }
        }

        private FacebookClient client;
        private FacebookClient Fb
        {
            get
            {
                if (client == null)
                {
                    client = new FacebookClient();
                }
                return client;
            }
        }
        #endregion

        public string LoginUrl(string callbackUrl)
        {
            var loginUrl = Fb.GetLoginUrl(new
            {
                client_id = this.AppID,
                client_secret = this.AppSecret,
                redirect_uri = callbackUrl,
                response_type = "code",
                scope = "email,publish_actions,public_profile"
            });

            return loginUrl.AbsoluteUri;
        }

        public dynamic GetUserProfile(string token)
        {
            Fb.AccessToken = token;
            return Fb.Get("me");
        }

        public dynamic Post(string userId, string token, string message, string link = "http://wakeorwaste.azurewebsites.net/")
        {
            Fb.AccessToken = token;
            return Fb.Post(string.Format("/{0}/feed", userId), new
            {
                client_id = this.AppID,
                client_secret = this.AppSecret,
                message = message,
                link = link
            });
        }

        public string GetAccessToken(string code, string redirectUri)
        {
            dynamic result = Fb.Post("oauth/access_token", new
            {
                client_id = this.AppID,
                client_secret = this.AppSecret,
                redirect_uri = redirectUri,
                code = code
            });
            return result.access_token;
        }
    }
}
