using SeriesStats.Core.Models.Core;
using SeriesStats.Core.Models.Trakt;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriesStats.Core.Services.Trakt.Abstractions
{
    public interface ITraktShowService
    {
        Task<IList<TraktLastWatchedShow>> GetWatchedShows(bool forceRefresh = false);
        IAsyncEnumerable<TraktShow> GetMyShows(FilterOptions filter, SortOptions sortOptions = SortOptions.ByName, bool forceRefresh = false);
        Task<TraktShowProgress> GetShowProgress(int traktId, bool forceRefresh = false);
    }
}