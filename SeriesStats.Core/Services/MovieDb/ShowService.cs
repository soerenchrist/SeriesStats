using SeriesStats.Core.Models.MovieDb;
using SeriesStats.Core.Models.MovieDb.Responses;
using SeriesStats.Core.Models.MovieDb.Shows;
using SeriesStats.Core.Services.MovieDb.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using SeriesStats.Core.Util.Abstractions;

namespace SeriesStats.Core.Services.MovieDb
{
    public class ShowService : IShowService
    {
        private readonly IGenreService _genreService;
        private readonly ICachedHttpHelper _cachedHttpHelper;
        private readonly IApiConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ShowService(IGenreService genreService, ICachedHttpHelper cachedHttpHelper,
            IApiConfiguration configuration)
        {
            _genreService = genreService;
            _cachedHttpHelper = cachedHttpHelper;
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<MovieDbTrendingItem>> GetTrendingShows()
        {
            var url = $"https://api.themoviedb.org/3/trending/tv/week?api_key={_configuration.TmdbApiKey}";

            var response = await _cachedHttpHelper.Fetch<MovieDbTrendingItemResponse>(_httpClient, url, TimeSpan.FromHours(1));
            if (response == null) return new List<MovieDbTrendingItem>();
            var shows = response.Results;
            foreach (var show in shows)
            {
                show.Genres = new List<Genre>();
                foreach (var genreId in show.GenreIds.Take(2))
                {
                    Genre genre = await _genreService.GetGenreById(genreId);
                    if (genre != null)
                        show.Genres.Add(genre);
                }
            }

            return shows;
        }

        public async Task<MovieDbShowDetail> GetShowDetail(int id)
        {
            var url = $"https://api.themoviedb.org/3/tv/{id}?api_key={_configuration.TmdbApiKey}&language=en-US";

            var result = await _cachedHttpHelper.Fetch<MovieDbShowDetail>(_httpClient, url, TimeSpan.FromDays(1));

            return result;
        }

        public async Task<IList<Cast>> GetCast(int showId)
        {
            var url = $"https://api.themoviedb.org/3/tv/{showId}/credits?api_key={_configuration.TmdbApiKey}&language=en-US";

            var result = await _cachedHttpHelper.Fetch<CreditsResponse>(_httpClient, url, TimeSpan.FromDays(10));
            if (result == null) return new List<Cast>();
            return result.Cast;
        }

        public async Task<IList<MovieDbTrendingItem>> SearchShows(string searchText)
        {
            var query = HttpUtility.UrlPathEncode(searchText);

            var url =
                $"https://api.themoviedb.org/3/search/tv?api_key={_configuration.TmdbApiKey}&language=en-US&query={query}&page=1&include_adult=false";
            var results = await _cachedHttpHelper.Fetch<MovieDbTrendingItemResponse>(_httpClient, url, TimeSpan.FromDays(1));

            if (results == null) return new List<MovieDbTrendingItem>();
            return results.Results;
        }

        public async Task<MovieDbEpisode> GetEpisode(int showId, int season, int episode)
        {
            var url =
                $"https://api.themoviedb.org/3/tv/{showId}/season/{season}/episode/{episode}?api_key={_configuration.TmdbApiKey}&language=en";
            var result = await _cachedHttpHelper.Fetch<MovieDbEpisode>(_httpClient, url, TimeSpan.FromDays(10));

            return result;
        }

        public async Task<IList<MovieDbEpisode>> GetEpisodes(int showId, int season)
        {
            var url =
                $"https://api.themoviedb.org/3/tv/{showId}/season/{season}?api_key={_configuration.TmdbApiKey}&language=en-US";
            var result = await _cachedHttpHelper.Fetch<Season>(_httpClient, url, TimeSpan.FromDays(1));
            return result?.Episodes ?? new List<MovieDbEpisode>();
        }
    }
}