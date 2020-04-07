using Prism.Commands;
using Prism.Navigation;
using SeriesStats.Core.Models.MovieDb.Shows;
using SeriesStats.Core.Services.MovieDb.Abstractions;
using SeriesStats.ViewModels.Base;
using SeriesStats.Views.Explore;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SeriesStats.ViewModels.Explore
{
    public class ExplorePageViewModel : ViewModelBase
    {
        private readonly IShowService _showService;
        private readonly IMovieService _movieService;
        public int SelectedItemId { get; set; }

        public ObservableCollection<MovieDbTrendingItem> TrendingShows { get; set; }
        public ObservableCollection<MovieDbTrendingItem> TrendingMovies { get; set; }
        public ICommand ShowSelectedCommand { get; }
        public ICommand MovieSelectedCommand { get; }
        public ICommand SearchCommand { get; }

        public ExplorePageViewModel(INavigationService navigationService,
            IShowService showService,
            IMovieService movieService) : base(navigationService)
        {
            _showService = showService;
            _movieService = movieService;

            ShowSelectedCommand = new DelegateCommand<MovieDbTrendingItem>(ShowSelected);
            MovieSelectedCommand = new DelegateCommand<MovieDbTrendingItem>(MovieSelected);
            SearchCommand = new DelegateCommand(Search);
            TrendingShows = new ObservableCollection<MovieDbTrendingItem>();
        }

        private void Search()
        {
            NavigationService.NavigateAsync(nameof(SearchPage), useModalNavigation: true);
        }

        private async void MovieSelected(MovieDbTrendingItem item)
        {
            SelectedItemId = item.Id;
            var navParam = new NavigationParameters { { nameof(item), item } };
            await NavigationService.NavigateAsync($"{nameof(MovieOverviewPage)}", navParam);
        }

        private async void ShowSelected(MovieDbTrendingItem item)
        {
            SelectedItemId = item.Id;
            var navParam = new NavigationParameters { { nameof(item), item } };
            await NavigationService.NavigateAsync($"{nameof(ShowOverviewPage)}", navParam);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            IsBusy = true;
            var tasks = new[]
            {
                GetTrendingMovies(),
                GetTrendingShows()
            };
            await Task.WhenAll(tasks);
            IsBusy = false;
        }

        private async Task GetTrendingShows()
        {
            try
            {
                var results = await _showService.GetTrendingShows();
                TrendingShows = new ObservableCollection<MovieDbTrendingItem>(results);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task GetTrendingMovies()
        {
            try
            {
                var results = await _movieService.GetTrendingMovies();
                TrendingMovies = new ObservableCollection<MovieDbTrendingItem>(results);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }
}