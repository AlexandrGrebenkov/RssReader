using System;
using Xamarin.Forms;
using System.Globalization;

namespace Helpers.Converters
{
    /* Конверторы визуального отображения
     * (Видимость, цвет, и т.д.)
     * */

    public class CanParseColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool CanParse = (bool)value;
            Color clr = new Color();
            if (CanParse)
                clr = Color.Green;
            else
                clr = Color.Red;
            return clr;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class NegativeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = (double)value;
            double nval = -1.0 * val;
            return nval;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class VisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool visible = false;
            switch (value)
            {
                case int tmp:
                {
                    if (tmp == 0)
                        visible = true;
                    break;
                }

                case bool tmp:
                {
                    visible = tmp;
                    break;
                }
            }
            if (String.Compare(((string)parameter), "false") == 0)
                visible = !visible;
            return visible;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class VisibleStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool visible = false;
            int.TryParse((string)parameter, out int res);
            if ((int)value == res)
                visible = true;

            return visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }  
}
