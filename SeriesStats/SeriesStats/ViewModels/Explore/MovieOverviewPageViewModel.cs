using Prism.AppModel;
using Prism.Navigation;
using SeriesStats.Core.Models.MovieDb;
using SeriesStats.Core.Models.MovieDb.Movies;
using SeriesStats.Core.Models.MovieDb.Shows;
using SeriesStats.Core.Services.MovieDb.Abstractions;
using SeriesStats.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriesStats.ViewModels.Explore
{
    public class MovieOverviewPageViewModel : ViewModelBase, IAutoInitialize
    {
        private readonly IMovieService _movieService;
        public MovieDbTrendingItem Item { get; set; }
        public MovieDbMovieDetail MovieDetail { get; set; }
        public IList<Cast> Cast { get; set; }
        public MovieOverviewPageViewModel(INavigationService navigationService,
            IMovieService movieService) : base(navigationService)
        {
            _movieService = movieService;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            IsBusy = true;
            try
            {
                var tasks = new[] { LoadDetail(), LoadCast() };
                await Task.WhenAll(tasks);
            }
            catch (Exception)
            {
            }
            IsBusy = false;
        }

        private async Task LoadDetail()
        {
            var detail = await _movieService.GetMovieDetail(Item.Id);
            MovieDetail = detail;
        }

        private async Task LoadCast()
        {
            var cast = await _movieService.GetCast(Item.Id);
            Cast = cast;
        }
    }
}