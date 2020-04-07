using Prism.Commands;
using Prism.Navigation;
using SeriesStats.Core.Models.Core;
using SeriesStats.Core.Models.Trakt;
using SeriesStats.Core.Services.Trakt.Abstractions;
using SeriesStats.Core.Util;
using SeriesStats.ViewModels.Base;
using SeriesStats.Views.MyShows;
using SeriesStats.Views.Popups;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace SeriesStats.ViewModels.MyShows
{
    public class MyShowsPageViewModel : ViewModelBase
    {
        private readonly ITraktShowService _showService;

        public ObservableCollection<TraktShow> Shows { get; set; }
        public ICommand ShowSelectedCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand FilterCommand { get; }
        public MyShowsPageViewModel(INavigationService navigationService,
            ITraktShowService showService) : base(navigationService)
        {
            _showService = showService;
            ShowSelectedCommand = new DelegateCommand<TraktShow>(ShowSelected);
            RefreshCommand = new DelegateCommand(Refresh);
            FilterCommand = new DelegateCommand(Filter);
        }

        private void Filter()
        {
            NavigationService.NavigateAsync(nameof(FilterPopupPage));
        }

        private async void Refresh()
        {
            IsRefreshing = true;
            IsBusy = true;
            await GetShows(true);
            IsRefreshing = false;
            IsBusy = false;
        }

        private void ShowSelected(TraktShow show)
        {
            var navParam = new NavigationParameters { { nameof(show), show } };
            NavigationService.NavigateAsync(nameof(MyShowDetailPage), navParam);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            IsBusy = true;

            var tasks = new[] { GetShows() };
            await Task.WhenAll(tasks).ConfigureAwait(false);
            IsBusy = false;
        }


        private async Task GetShows(bool forceRefresh = false)
        {
            var sortKey = Preferences.Get(PreferenceKeys.SortKey, SortOptions.ByLastViewed.ToString());
            var options = (SortOptions)Enum.Parse(typeof(SortOptions), sortKey);
            var filterKey = Preferences.Get(PreferenceKeys.FilterKey, "");
            var filterOptions = FilterOptions.FromString(filterKey);

            var shows = _showService.GetMyShows(filterOptions, options, forceRefresh);
            Shows = new ObservableCollection<TraktShow>();
            await foreach (var show in shows)
            {
                Shows.Add(show);
            }
        }
    }
}