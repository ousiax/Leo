// MIT License

using System.Globalization;
using System.Windows.Data;

namespace Leo.Wpf.App.Converters
{
    public sealed class BooleanInverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                return !b;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                return !b;
            }

            return value;
        }
    }
}
