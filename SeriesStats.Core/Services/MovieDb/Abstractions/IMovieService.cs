using SeriesStats.Core.Models.MovieDb;
using SeriesStats.Core.Models.MovieDb.Movies;
using SeriesStats.Core.Models.MovieDb.Shows;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriesStats.Core.Services.MovieDb.Abstractions
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDbTrendingItem>> GetTrendingMovies();
        Task<MovieDbMovieDetail> GetMovieDetail(int id);
        Task<IList<Cast>> GetCast(int movieId);
        Task<IList<MovieDbTrendingItem>> SearchMovies(string searchTerm);
    }
}