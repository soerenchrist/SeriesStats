using SeriesStats.Util;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace SeriesStats.Converters
{
    public class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var minutes = (int)value;
            return TimeHelper.ParseMinutes(minutes);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}