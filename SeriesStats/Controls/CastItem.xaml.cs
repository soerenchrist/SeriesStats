using SeriesStats.Converters;
using SeriesStats.Core.Models.MovieDb;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeriesStats.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CastItem
    {

        public static readonly BindableProperty CastProperty =
            BindableProperty.Create(nameof(Cast), typeof(Cast), typeof(CastItem),
                null, BindingMode.OneWay, propertyChanged: CastPropertyChanged);


        public Cast Cast {
            get => (Cast)GetValue(CastProperty);
            set => SetValue(CastProperty, value);
        }
        public CastItem()
        {
            InitializeComponent();
        }

        public CastItem(Cast cast) : this()
        {
            Cast = cast;
        }

        private static void CastPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is CastItem component)) return;

            if (!(newvalue is Cast cast)) return;

            var converter = new AbsolutePathConverter();
            var imageSource = converter.Convert(cast.ProfilePath, typeof(string), "w300", CultureInfo.CurrentCulture)
                .ToString();
            component.CastImage.Source = imageSource;
            component.NameLabel.Text = cast.Name;
            component.CharacterLabel.Text = cast.Character;
        }


    }
}