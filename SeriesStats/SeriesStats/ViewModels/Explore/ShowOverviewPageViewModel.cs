using Prism.AppModel;
using Prism.Navigation;
using SeriesStats.Core.Models.MovieDb;
using SeriesStats.Core.Models.MovieDb.Shows;
using SeriesStats.Core.Services.MovieDb.Abstractions;
using SeriesStats.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriesStats.ViewModels.Explore
{
    public class ShowOverviewPageViewModel : ViewModelBase, IAutoInitialize
    {
        private readonly IShowService _showService;
        public MovieDbTrendingItem Item { get; set; }
        public MovieDbShowDetail ShowDetail { get; set; }
        public IList<Cast> Cast { get; set; } = new List<Cast>();
        public ShowOverviewPageViewModel(INavigationService navigationService,
            IShowService showService) : base(navigationService)
        {
            _showService = showService;
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
            var detail = await _showService.GetShowDetail(Item.Id);
            ShowDetail = detail;
        }

        private async Task LoadCast()
        {
            var cast = await _showService.GetCast(Item.Id);
            Cast = cast;
        }
    }
}