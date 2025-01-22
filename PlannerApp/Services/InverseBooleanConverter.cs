using System;
using Avalonia.Data.Converters;
using System.Globalization;

namespace PlannerApp.Services
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool boolValue && !boolValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool boolValue && !boolValue;
        }
    }
}
