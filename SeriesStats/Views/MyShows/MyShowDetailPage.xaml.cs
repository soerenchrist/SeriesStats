
using Xamarin.Forms.Xaml;

namespace SeriesStats.Views.MyShows
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyShowDetailPage
    {
        public MyShowDetailPage()
        {
            InitializeComponent();

            CurrentPage = Children[1];
        }
    }
}