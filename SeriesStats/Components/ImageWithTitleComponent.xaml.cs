using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeriesStats.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageWithTitleComponent
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(ImageWithTitleComponent),
                null, BindingMode.OneWay, propertyChanged: TitlePropertyChanged);

        public string Title {
            get => GetValue(TitleProperty).ToString();
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(ImageWithTitleComponent),
                null, BindingMode.OneWay, propertyChanged: ImageSourcePropertyChanged);

        public ImageSource ImageSource {
            get => (ImageSource)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        private static void TitlePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var component = (ImageWithTitleComponent)bindable;
            var title = newvalue?.ToString();
            if (title == null) return;

            component.ShowNameLabel.Text = title;
        }
        private static void ImageSourcePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var component = (ImageWithTitleComponent)bindable;
            var image = newvalue as ImageSource;

            if (image == null) return;

            component.BackdropImage.Source = image;
        }

        public ImageWithTitleComponent()
        {
            InitializeComponent();
        }
    }
}