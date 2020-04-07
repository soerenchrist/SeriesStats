using SeriesStats.ViewModels;
using Xamarin.Forms.Xaml;

namespace SeriesStats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            // Need to call this manually, because menu page is not navigated to
            var viewModel = BindingContext as MenuPageViewModel;
            viewModel?.OnNavigatedTo(null);
        }
    }
}