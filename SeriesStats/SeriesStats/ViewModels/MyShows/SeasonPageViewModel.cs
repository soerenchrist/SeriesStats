using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;
using SeriesStats.Core.Models.MovieDb;
using SeriesStats.Core.Models.MovieDb.Shows;
using SeriesStats.Core.Services.MovieDb.Abstractions;
using SeriesStats.ViewModels.Base;
using SeriesStats.Views.MyShows;
using System.Collections.Generic;
using System.Windows.Input;

namespace SeriesStats.ViewModels.MyShows
{
    public class SeasonPageViewModel : ViewModelBase, IAutoInitialize
    {
        private readonly IShowService _showService;
        public Season Season { get; set; }
        public MovieDbShowDetail Show { get; set; }
        public IList<MovieDbEpisode> Episodes { get; set; }

        public ICommand EpisodeSelectedCommand { get; }

        public SeasonPageViewModel(INavigationService navigationService,
            IShowService showService) : base(navigationService)
        {
            _showService = showService;
            EpisodeSelectedCommand = new DelegateCommand<MovieDbEpisode>(EpisodeSelected);
        }

        private void EpisodeSelected(MovieDbEpisode episode)
        {
            var navParams = new NavigationParameters
            {
                {nameof(Show), Show},
                {"SelectedEpisode", episode},
                {"Episodes", Episodes},
                {"Season", Season}
            };
            NavigationService.NavigateAsync(nameof(EpisodesPage), navParams);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Episodes = await _showService.GetEpisodes(Show.Id, Season.SeasonNumber);
        }

    }
}