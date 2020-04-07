using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace SeriesStats.Components
{
    public partial class CardWithOverviewComponent : PancakeView
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(CardWithOverviewComponent),
                null, BindingMode.OneWay, propertyChanged: TitlePropertyChanged);
        public static readonly BindableProperty OverviewProperty =
            BindableProperty.Create(nameof(Overview), typeof(string), typeof(CardWithOverviewComponent),
                null, BindingMode.OneWay, propertyChanged: OverviewPropertyChanged);

        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(CardWithOverviewComponent),
                null, BindingMode.OneWay, propertyChanged: ImageSourcePropertyChanged);

        public string Title {
            get => GetValue(TitleProperty).ToString();
            set => SetValue(TitleProperty, value);
        }

        public string Overview {
            get => GetValue(OverviewProperty).ToString();
            set => SetValue(OverviewProperty, value);
        }

        public ImageSource ImageSource {
            get => (ImageSource)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public CardWithOverviewComponent()
        {
            InitializeComponent();
        }

        private static void TitlePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var component = (CardWithOverviewComponent)bindable;
            var title = newvalue?.ToString();
            if (title == null) return;

            component.TitleLabel.Text = title;
        }

        private static void OverviewPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var component = (CardWithOverviewComponent)bindable;
            var overview = newvalue?.ToString();
            if (overview == null) return;

            component.OverviewLabel.Text = overview;
        }
        private static void ImageSourcePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var component = (CardWithOverviewComponent)bindable;
            var image = newvalue as ImageSource;

            if (image == null) return;

            component.Image.Source = image;
        }
    }
}
