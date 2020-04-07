using SeriesStats.Core.Models.MovieDb;
using SeriesStats.Core.Models.MovieDb.Shows;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriesStats.Core.Services.MovieDb.Abstractions
{
    public interface IShowService
    {
        Task<IEnumerable<MovieDbTrendingItem>> GetTrendingShows();
        Task<MovieDbShowDetail> GetShowDetail(int id);
        Task<IList<Cast>> GetCast(int showId);
        Task<IList<MovieDbTrendingItem>> SearchShows(string searchText);
        Task<MovieDbEpisode> GetEpisode(int showId, int season, int episode);
        Task<IList<MovieDbEpisode>> GetEpisodes(int showId, int season);
    }
}