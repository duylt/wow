using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Wow.MobileApp
{
    public class ChallengeDayDetailConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isCompleted = System.Convert.ToBoolean(value);
            if (isCompleted)
            {
                return "#FF3DCD26";
            }
            else
            {
                return "#FF9B9B9B";
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
