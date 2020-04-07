using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;
using SeriesStats.Core.Models.MovieDb;
using SeriesStats.Core.Models.MovieDb.Shows;
using SeriesStats.Core.Models.Trakt;
using SeriesStats.Core.Services.Core.Abstractions;
using SeriesStats.Core.Services.MovieDb.Abstractions;
using SeriesStats.Core.Services.Trakt.Abstractions;
using SeriesStats.ViewModels.Base;
using SeriesStats.Views.MyShows;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SeriesStats.ViewModels.MyShows
{
    public class MyShowDetailPageViewModel : ViewModelBase, IAutoInitialize
    {
        private readonly ITraktShowService _traktShowService;
        private readonly IShowService _movieDbShowService;
        private readonly IExternalOpener _externalOpener;
        private readonly ITraktCheckinService _traktCheckinService;
        public MovieDbEpisode NextEpisode { get; set; }
        public IList<Cast> Cast { get; set; } = new List<Cast>();
        public TraktShow Show { get; set; }
        public TraktShowProgress ShowProgress { get; set; }
        public MovieDbShowDetail ShowDetail { get; set; }
        public bool HasNewEpisode => NextEpisode != null && !IsBusy;

        public ICommand SeasonSelectedCommand { get; }
        public ICommand SetEpisodeWatchedCommand { get; }
        public ICommand OpenExternallyCommand { get; }
        public ICommand OpenShowExternallyCommand { get; }

        public MyShowDetailPageViewModel(INavigationService navigationService,
            ITraktShowService traktShowService,
            IShowService movieDbShowService,
            IExternalOpener externalOpener,
            ITraktCheckinService traktCheckinService) : base(navigationService)
        {
            _traktShowService = traktShowService;
            _movieDbShowService = movieDbShowService;
            _externalOpener = externalOpener;
            _traktCheckinService = traktCheckinService;

            SeasonSelectedCommand = new DelegateCommand<Season>(SeasonSelected);
            SetEpisodeWatchedCommand = new DelegateCommand(SetEpisodeWatched);
            OpenExternallyCommand = new DelegateCommand<string>(OpenExternally);
            OpenShowExternallyCommand = new DelegateCommand<string>(OpenShowExternally);
        }

        private void OpenExternally(string type)
        {
            switch (type)
            {
                case "trakt":
                    _externalOpener.OpenEpisodeOnTrakt(Show.Ids.Slug, NextEpisode.SeasonNumber,
                        NextEpisode.EpisodeNumber);
                    break;
                case "tmdb":
                    _externalOpener.OpenEpisodeOnMovieDb(ShowDetail.Id, NextEpisode.SeasonNumber,
                        NextEpisode.EpisodeNumber);
                    break;
            }
        }
        private void OpenShowExternally(string type)
        {
            switch (type)
            {
                case "trakt":
                    _externalOpener.OpenShowOnTrakt(Show.Ids.Slug);
                    break;
                case "tmdb":
                    _externalOpener.OpenShowOnMovieDb(ShowDetail.Id);
                    break;
            }
        }

        private async void SetEpisodeWatched()
        {
            var isSuccessful = await _traktCheckinService.CheckinEpisode(ShowProgress.NextEpisode, DateTimeOffset.Now);
            if (isSuccessful)
            {
                Messages.ShortAlert($"Episode {NextEpisode.EpisodeNumber} watched!");
            }
            else
            {
                Messages.ShortAlert("Could not set episode watched");
            }
        }

        private void SeasonSelected(Season season)
        {
            var parameters = new NavigationParameters
            {
                {nameof(season), season},
                {"show", ShowDetail}
            };
            NavigationService.NavigateAsync(nameof(SeasonPage), parameters);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            IsBusy = true;
            var tasks = new[] { GetProgress(), GetDetail(), GetCast() };
            await Task.WhenAll(tasks);
            IsBusy = false;
            RaisePropertyChanged(nameof(HasNewEpisode));
        }

        private async Task GetDetail()
        {
            if (Show.Ids.Tmdb.HasValue)
                ShowDetail = await _movieDbShowService.GetShowDetail(Show.Ids.Tmdb.Value);
        }

        private async Task GetProgress()
        {
            ShowProgress = await _traktShowService.GetShowProgress(Show.Ids.Trakt);
            if (ShowProgress.NextEpisode != null && Show.Ids.Tmdb.HasValue)
            {
                var episode = ShowProgress.NextEpisode;
                NextEpisode = await _movieDbShowService.GetEpisode(Show.Ids.Tmdb.Value, episode.Season, episode.Number);
            }
        }

        private async Task GetCast()
        {
            if (Show.Ids.Tmdb.HasValue)
                Cast = await _movieDbShowService.GetCast(Show.Ids.Tmdb.Value);
        }
    }

}