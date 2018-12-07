using System;
using Xamarin.Forms;
using System.Globalization;

namespace Helpers.Converters
{
    /* Конверторы преобразования основных типов данных 
     * (Числа, с плавующей точкой, проценты, и т.д.)
     * */

    class PercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = String.Empty;

            string format = "##";
            if (parameter != null)
            {
                format = (string)parameter;
            }

            switch (value)
            {
                case decimal tmp:
                {
                    result = tmp.ToString(format);
                    break;
                }
                case double tmp:
                {
                    result = tmp.ToString(format);
                    break;
                }
                case float tmp:
                {
                    result = tmp.ToString(format);
                    break;
                }
                case int tmp:
                {
                    result = tmp.ToString(format);
                    break;
                }
            }
            return result + "%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class FloatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = String.Empty;

            string format = "F3";
            if (parameter != null)
            {
                int.TryParse((string)parameter, out int res);
                format = "F" + res.ToString();
            }

            switch (value)
            {
                case decimal tmp:   result = tmp.ToString(format); break;
                case double tmp:    result = tmp.ToString(format); break;
                case float tmp:     result = tmp.ToString(format); break;
                case int tmp:       result = tmp.ToString(format); break;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            if (string.IsNullOrEmpty(strValue))
                strValue = "0";
            float resultFloat;
            if (float.TryParse(strValue.Replace(',', '.'), out resultFloat))
            {
                return resultFloat;
            }
            return 0;
        }
    }
}
