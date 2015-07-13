using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoW.Core.Constract
{
    public interface ISocial
    {
        string LoginUrl(string callbackUrl);

        dynamic GetUserProfile(string token);

        dynamic Post(string userId, string token, string message, string link);

        string GetAccessToken(string code, string redirectUri);
    }
}
