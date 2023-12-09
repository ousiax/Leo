using Leo.Data.Domain.Entities;
using Leo.Wpf.App.Infrastructure;
using System.Globalization;
using System.Windows.Data;

namespace Leo.Wpf.App.Converters
{
    public class GenderToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string key)
            {
                return Constants.Genders[key];
            }
            else if (value is Gender gender)
            {
                return Constants.Genders[gender.ToString()];
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
