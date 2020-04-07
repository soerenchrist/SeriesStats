using SeriesStats.Core.Util;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace SeriesStats.Converters
{
    public class AbsolutePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";
            var relativePath = value.ToString();
            var size = parameter == null ? "w500" : parameter.ToString();
            return $"{Constants.TheMovieDbBasePath}/{size}{relativePath}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}