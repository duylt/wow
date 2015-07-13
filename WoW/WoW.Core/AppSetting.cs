using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace WoW.Core
{
    public static class AppSetting
    {
        public static string Social(string name)
        {
            var setting = ConfigurationManager.ConnectionStrings[name];
            if (setting == null)
            {
                throw new Exception("Missing connection string for " + name);
            }
            else
            {
                return setting.ConnectionString;
            }
        }
    }
}
