using Prism.AppModel;
using Prism.Navigation;
using SeriesStats.Core.Models.MovieDb;
using SeriesStats.Core.Models.MovieDb.Shows;
using SeriesStats.ViewModels.Base;
using System.Collections.Generic;

namespace SeriesStats.ViewModels.MyShows
{
    public class EpisodesPageViewModel : ViewModelBase, IAutoInitialize
    {
        public MovieDbEpisode SelectedEpisode { get; set; }
        public MovieDbShowDetail Show { get; set; }
        public IList<MovieDbEpisode> Episodes { get; set; }
        public Season Season { get; set; }
        public int SelectedIndex { get; set; }
        public EpisodesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            SelectedIndex = Episodes.IndexOf(SelectedEpisode);
        }
    }
}