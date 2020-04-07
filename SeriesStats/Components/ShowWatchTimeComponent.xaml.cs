using SeriesStats.Converters;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace SeriesStats.Components
{
    public partial class ShowWatchTimeComponent
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(ShowWatchTimeComponent),
                null, BindingMode.OneWay, propertyChanged: TitlePropertyChanged);

        public static readonly BindableProperty PlayNumberProperty =
            BindableProperty.Create(nameof(PlayNumber), typeof(int), typeof(ShowWatchTimeComponent),
                null, BindingMode.OneWay, propertyChanged: PlayNumberPropertyChanged);

        public static readonly BindableProperty MinutesPlayedProperty =
            BindableProperty.Create(nameof(MinutesPlayed), typeof(int), typeof(ShowWatchTimeComponent),
                null, BindingMode.OneWay, propertyChanged: MinutesPlayedPropertyChanged);

        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(ShowWatchTimeComponent),
                null, BindingMode.OneWay, propertyChanged: ImageSourcePropertyChanged);

        public string Title {
            get => GetValue(TitleProperty).ToString();
            set => SetValue(TitleProperty, value);
        }

        public int PlayNumber {
            get => (int)GetValue(PlayNumberProperty);
            set => SetValue(PlayNumberProperty, value);
        }
        public int MinutesPlayed {
            get => (int)GetValue(MinutesPlayedProperty);
            set => SetValue(MinutesPlayedProperty, value);
        }


        public ImageSource ImageSource {
            get => (ImageSource)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public ShowWatchTimeComponent()
        {
            InitializeComponent();
        }

        private static void TitlePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var component = (ShowWatchTimeComponent)bindable;
            var title = newvalue?.ToString();
            if (title == null) return;

            component.TitleLabel.Text = title;
        }

        private static void PlayNumberPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var component = (ShowWatchTimeComponent)bindable;
            var number = (int)newvalue;

            component.PlayNumberLabel.Text = $"Number of episodes watched: {number}";
        }

        private static void MinutesPlayedPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var component = (ShowWatchTimeComponent)bindable;
            var minutes = (int)newvalue;

            var converter = new TimeConverter();
            var minutesString = converter.Convert(minutes, typeof(string), null, CultureInfo.CurrentCulture).ToString();

            component.WatchTimeLabel.Text = $"Total time: {minutesString}";
        }
        private static void ImageSourcePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var component = (ShowWatchTimeComponent)bindable;
            var image = newvalue as ImageSource;

            if (image == null) return;

            component.Image.Source = image;
        }
    }
}
