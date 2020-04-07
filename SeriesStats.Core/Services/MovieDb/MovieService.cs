using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using SeriesStats.Core.Models.MovieDb;
using SeriesStats.Core.Models.MovieDb.Movies;
using SeriesStats.Core.Models.MovieDb.Responses;
using SeriesStats.Core.Models.MovieDb.Shows;
using SeriesStats.Core.Services.MovieDb.Abstractions;
using SeriesStats.Core.Util;
using SeriesStats.Core.Util.Abstractions;

namespace SeriesStats.Core.Services.MovieDb
{
    public class MovieService : IMovieService
    {
        private readonly IGenreService _genreService;
        private readonly ICachedHttpHelper _cachedHttpHelper;
        private readonly IApiConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public MovieService(IGenreService genreService, ICachedHttpHelper cachedHttpHelper,
            IApiConfiguration configuration)
        {
            _genreService = genreService;
            _cachedHttpHelper = cachedHttpHelper;
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<MovieDbTrendingItem>> GetTrendingMovies()
        {
            var url = $"https://api.themoviedb.org/3/trending/movie/week?api_key={_configuration.TmdbApiKey}";

            var response = await _cachedHttpHelper.Fetch<MovieDbTrendingItemResponse>(_httpClient, url, TimeSpan.FromHours(1));
            if (response == null) return new List<MovieDbTrendingItem>();
            var movies = response.Results;
            foreach (var show in movies)
            {
                show.Genres = new List<Genre>();
                foreach (var genreId in show.GenreIds.Take(2))
                {
                    Genre genre = await _genreService.GetGenreById(genreId);
                    if (genre != null)
                        show.Genres.Add(genre);
                }
            }

            return movies;
        }

        public async Task<MovieDbMovieDetail> GetMovieDetail(int id)
        {
            var url = $"https://api.themoviedb.org/3/movie/{id}?api_key={_configuration.TmdbApiKey}&language=en-US";

            var result = await _cachedHttpHelper.Fetch<MovieDbMovieDetail>(_httpClient, url, TimeSpan.FromDays(1));

            return result;
        }

        public async Task<IList<Cast>> GetCast(int movieId)
        {
            var url = $"https://api.themoviedb.org/3/movie/{movieId}/credits?api_key={_configuration.TmdbApiKey}&language=en-US";

            var result = await _cachedHttpHelper.Fetch<CreditsResponse>(_httpClient, url, TimeSpan.FromDays(10));
            if (result == null) return new List<Cast>();
            return result.Cast;
        }

        public async Task<IList<MovieDbTrendingItem>> SearchMovies(string searchTerm)
        {
            var query = HttpUtility.UrlPathEncode(searchTerm);

            var url =
                $"https://api.themoviedb.org/3/search/movie?api_key={_configuration.TmdbApiKey}&language=en-US&query={query}&page=1&include_adult=false";
            var results = await _cachedHttpHelper.Fetch<MovieDbTrendingItemResponse>(_httpClient, url, TimeSpan.FromDays(1));

            if (results == null) return new List<MovieDbTrendingItem>();
            return results.Results;

        }
    }
}