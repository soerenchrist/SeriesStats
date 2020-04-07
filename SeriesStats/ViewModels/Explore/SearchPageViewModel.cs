using Prism.Commands;
using Prism.Navigation;
using SegmentedControl.FormsPlugin.Abstractions;
using SeriesStats.Core.Models.MovieDb.Shows;
using SeriesStats.Core.Services.MovieDb.Abstractions;
using SeriesStats.ViewModels.Base;
using SeriesStats.Views.Explore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SeriesStats.ViewModels.Explore
{
    public class SearchPageViewModel : ViewModelBase
    {
        private readonly IShowService _showService;
        private readonly IMovieService _movieService;
        public string SearchText { get; set; }
        public ICommand SearchItemTappedCommand { get; }
        public ICommand SearchTypeChangedCommand { get; }
        public ObservableCollection<MovieDbTrendingItem> ShowResults { get; set; }
        public ObservableCollection<MovieDbTrendingItem> MovieResults { get; set; }
        public bool ShowShows { get; set; } = true;
        public SearchPageViewModel(INavigationService navigationService,
            IShowService showService,
            IMovieService movieService) : base(navigationService)
        {
            _showService = showService;
            _movieService = movieService;
            SearchItemTappedCommand = new DelegateCommand<MovieDbTrendingItem>(ItemTapped);
            SearchTypeChangedCommand = new DelegateCommand<ValueChangedEventArgs>(SearchTypeChanged);
            PropertyChanged += ViewModelPropertyChanged;
        }

        private void SearchTypeChanged(ValueChangedEventArgs obj)
        {
            ShowShows = obj.NewValue == 0;
        }

        private async void ItemTapped(MovieDbTrendingItem item)
        {
            var navParam = new NavigationParameters { { nameof(item), item } };
            if (ShowShows)
                await NavigationService.NavigateAsync($"{nameof(ShowOverviewPage)}", navParam, useModalNavigation: false);
            else
                await NavigationService.NavigateAsync($"{nameof(MovieOverviewPage)}", navParam);
        }

        public override void Destroy()
        {
            base.Destroy();
            PropertyChanged -= ViewModelPropertyChanged;
        }

        private async void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SearchText) && SearchText.Length > 4)
            {
                IsBusy = true;
                var tasks = new[] { SearchShows(), SearchMovies() };
                await Task.WhenAll(tasks);
                IsBusy = false;
            }
        }

        private async Task SearchShows()
        {
            var results = await _showService.SearchShows(SearchText);
            ShowResults = new ObservableCollection<MovieDbTrendingItem>(results);
        }

        private async Task SearchMovies()
        {
            var results = await _movieService.SearchMovies(SearchText);
            MovieResults = new ObservableCollection<MovieDbTrendingItem>(results);
        }
    }
}