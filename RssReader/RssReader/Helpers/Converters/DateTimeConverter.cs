using System;
using System.Globalization;
using Xamarin.Forms;

namespace Helpers.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dt)
            {
                if (dt == DateTime.MinValue || dt == DateTime.MaxValue)
                    return string.Empty;
                if (dt.Hour == 0 && dt.Minute == 0 && dt.Second == 0)
                    return dt.ToString(culture.DateTimeFormat.ShortDatePattern);
                return dt.ToString(culture.DateTimeFormat);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
