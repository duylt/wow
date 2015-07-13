using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoW.Core.Constract
{
    public class SocialBase<T>
    {
        protected string ConnectionString = AppSetting.Social(typeof(T).Name);

        public string Setting(string field)
        {
            var settingContent = ConnectionString.Split(';').FirstOrDefault(fieldContent => fieldContent.ToLower().Contains(field.ToLower()));
            if (settingContent != null && settingContent.Split('=').Count() == 2)
            {
                return settingContent.Split('=')[1].Trim();
            }
            else
            {
                throw new Exception("Missing connection string " + typeof(T).Name + "." + field);
            }
        }
    }
}
