using SeriesStats.Core.Models.Trakt;
using Xamarin.Forms;

namespace SeriesStats.Components
{
    public partial class MyShowComponent
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(MyShowComponent),
                null, BindingMode.OneWay, propertyChanged: TitlePropertyChanged);
        public static readonly BindableProperty ShowProgressProperty =
            BindableProperty.Create(nameof(ShowProgress), typeof(TraktShowProgress), typeof(MyShowComponent),
                null, BindingMode.OneWay, propertyChanged: ShowProgressPropertyChanged);

        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(MyShowComponent),
                null, BindingMode.OneWay, propertyChanged: ImageSourcePropertyChanged);

        public string Title {
            get => GetValue(TitleProperty).ToString();
            set => SetValue(TitleProperty, value);
        }

        public TraktShowProgress ShowProgress {
            get => (TraktShowProgress)GetValue(ShowProgressProperty);
            set => SetValue(ShowProgressProperty, value);
        }

        public ImageSource ImageSource {
            get => (ImageSource)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public MyShowComponent()
        {
            InitializeComponent();
        }

        private static void TitlePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var component = (MyShowComponent)bindable;
            var title = newvalue?.ToString();
            if (title == null) return;

            component.TitleLabel.Text = title;
        }

        private static void ShowProgressPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var component = (MyShowComponent)bindable;
            var progress = newvalue as TraktShowProgress;
            if (progress == null)
            {
                return;
            }

            if (progress.NextEpisode == null)
            {
                component.NextEpisodeLabel.Text = "No next episode";
            }
            else
            {
                component.NextEpisodeLabel.Text = $"S{progress.NextEpisode.Season}E{progress.NextEpisode.Number}: {progress.NextEpisode.Title}";
            }

            var aired = progress.Aired;
            var watched = progress.Completed;
            var percentage = (double)watched / aired;
            component.ShowProgressBar.ProgressTo(percentage, 500, Easing.Linear);
            component.ProgressLabel.Text = $"{watched} / {aired} episodes watched";
        }
        private static void ImageSourcePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var component = (MyShowComponent)bindable;
            var image = newvalue as ImageSource;

            if (image == null)
            {
                return;
            }


            component.Image.Source = image;
        }
    }
}
