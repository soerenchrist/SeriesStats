using System;
using System.Globalization;
using Xamarin.Forms;
using ValueChangedEventArgs = SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs;

namespace SeriesStats.Converters
{
    public class ValueChangedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var itemTappedEventArgs = value as ValueChangedEventArgs;
            if (itemTappedEventArgs == null)
            {
                throw new ArgumentException("Expected value to be of type ItemTappedEventArgs", nameof(value));
            }
            return itemTappedEventArgs;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}