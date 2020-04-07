using Prism.Navigation;
using SeriesStats.ViewModels.Base;

namespace SeriesStats.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MenuPageViewModel MenuPageViewModel { get; }
        public MainPageViewModel(INavigationService navigationService,
            MenuPageViewModel menuPageViewModel) : base(navigationService)
        {
            MenuPageViewModel = menuPageViewModel;
        }
    }
}