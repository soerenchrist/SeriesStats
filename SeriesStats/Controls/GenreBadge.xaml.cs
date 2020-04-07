using SeriesStats.Components;
using SeriesStats.Core.Models.MovieDb;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeriesStats.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenreBadge
    {
        public static readonly BindableProperty GenreProperty =
            BindableProperty.Create(nameof(Genre), typeof(Genre), typeof(ShowOverviewComponent),
                null, BindingMode.OneWay, propertyChanged: GenrePropertyChanged);
        public Genre Genre {
            get => (Genre)GetValue(GenreProperty);
            set => SetValue(GenreProperty, value);
        }
        public GenreBadge()
        {
            InitializeComponent();
        }

        public GenreBadge(Genre genre) : this()
        {
            Genre = genre;
        }

        private static void GenrePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var component = bindable as GenreBadge;
            if (component == null) return;
            var genre = newvalue as Genre;
            if (genre == null) return;

            component.NameLabel.Text = genre.Name;
        }

    }
}