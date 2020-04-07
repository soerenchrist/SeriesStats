using SeriesStats.Controls;
using SeriesStats.Converters;
using SeriesStats.Core.Models.MovieDb;
using SeriesStats.Core.Models.MovieDb.Shows;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeriesStats.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowOverviewComponent : Grid
    {
        public static readonly BindableProperty ShowProperty =
            BindableProperty.Create(nameof(Show), typeof(MovieDbShowDetail), typeof(ShowOverviewComponent),
                null, BindingMode.OneWay, propertyChanged: ShowPropertyChanged);

        public MovieDbShowDetail Show {
            get => (MovieDbShowDetail)GetValue(ShowProperty);
            set => SetValue(ShowProperty, value);
        }

        public static readonly BindableProperty CastProperty =
            BindableProperty.Create(nameof(Cast), typeof(IList<Cast>), typeof(ShowOverviewComponent),
                null, BindingMode.OneWay, propertyChanged: CastPropertyChanged);

        public IList<Cast> Cast {
            get => (List<Cast>)GetValue(CastProperty);
            set => SetValue(CastProperty, value);
        }
        public ShowOverviewComponent()
        {
            InitializeComponent();
        }

        private static void ShowPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var component = bindable as ShowOverviewComponent;
            if (component == null) return;
            var show = newvalue as MovieDbShowDetail;
            if (show == null) return;

            var converter = new AbsolutePathConverter();
            var imagePath = converter.Convert(show.BackdropPath, typeof(string), "w780", CultureInfo.CurrentCulture).ToString();

            component.ShowNameLabel.Text = show.Name;
            component.ShowCreatorInfoLabel.Text = show.FirstAirDate.Date.ToShortDateString();
            var creator = show.CreatedBy.FirstOrDefault();
            if (creator != null)
            {
                component.ShowCreatorInfoLabel.Text += $" - {creator.Name}";
            }
            component.VoteAverageLabel.Text = show.VoteAverage.ToString(CultureInfo.CurrentCulture);
            component.RatingsLabel.Text = show.VoteCount.ToString();
            component.OverviewLabel.Text = show.Overview;
            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                component.ShowImage.Source = new UriImageSource
                {
                    Uri = new Uri(imagePath)
                };
            }
            foreach (var genre in show.Genres)
            {
                component.GenresLayout.Children.Add(new GenreBadge(genre));
            }
        }

        private static void CastPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is ShowOverviewComponent component)) return;
            if (!(newvalue is IList<Cast> cast)) return;

            component.CastList.ItemsSource = cast;
        }
    }
}