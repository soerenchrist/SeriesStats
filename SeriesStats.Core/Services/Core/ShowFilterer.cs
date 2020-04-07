using SeriesStats.Core.Models.Core;
using SeriesStats.Core.Models.Trakt;
using SeriesStats.Core.Services.Core.Abstractions;

namespace SeriesStats.Core.Services.Core
{
    public class ShowFilterer : IShowFilterer
    {
        public bool FilterApplies(TraktShow show, FilterOptions options)
        {
            if (!options.Continued && !options.Favorite && !options.NotViewed)
                return true;
            if (show.IsFavorite && options.Favorite)
            {
                return true;
            }
            // TODO finish
            return false;
        }
    }
}