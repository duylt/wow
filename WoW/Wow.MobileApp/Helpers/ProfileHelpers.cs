using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Wow.MobileApp.Helpers
{
    public static class ProfileHelpers
    {


        public static Challenge MyChallenge
        {
            get
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                Object value = localSettings.Values["MyChallenge"];
                return null;

            }
            set
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                localSettings.Values["MyChallenge"] = value;
            }
        }
        public static bool IsLogin
        {
            get
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                Object value = localSettings.Values["IsLogin"];
                if (value == null || !Convert.ToBoolean(value))
                {
                    return false;
                }
                return true;

            }
            set
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                localSettings.Values["IsLogin"] = value;
            }
        }



        public static string ProfileImage
        {
            get
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                Object value = localSettings.Values["ProfileImage"];
                if (value == null)
                    return string.Empty;
                else return value.ToString();
            }
            set
            {

                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                localSettings.Values["ProfileImage"] = value;
            }
        }

        public static string ProfileName
        {
            get
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                Object value = localSettings.Values["ProfileName"];
                if (value == null)
                    return string.Empty;
                else return value.ToString();
            }
            set
            {

                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                localSettings.Values["ProfileName"] = value;
            }
        }

        public static bool HasSetAlarm
        {
            get
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                Object value = localSettings.Values["HasSetAlarm"];
                if (value == null || !Convert.ToBoolean(value))
                {
                    return false;
                }
                return true;
            }
            set
            {

                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                localSettings.Values["HasSetAlarm"] = value;
            }
        }

    }
}
