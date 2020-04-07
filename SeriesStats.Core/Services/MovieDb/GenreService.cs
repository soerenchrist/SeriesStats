using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MoreLinq;
using SeriesStats.Core.Models.MovieDb;
using SeriesStats.Core.Models.MovieDb.Responses;
using SeriesStats.Core.Services.MovieDb.Abstractions;
using SeriesStats.Core.Util;
using SeriesStats.Core.Util.Abstractions;

namespace SeriesStats.Core.Services.MovieDb
{
    public class GenreService : IGenreService
    {
        private readonly ICachedHttpHelper _cachedHttpHelper;
        private readonly IApiConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private List<Genre> _genres;

        public GenreService(ICachedHttpHelper cachedHttpHelper,
            IApiConfiguration configuration)
        {
            _genres = new List<Genre>();
            _cachedHttpHelper = cachedHttpHelper;
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        public async Task<Genre> GetGenreById(int genreId)
        {
            if (_genres.Count == 0)
            {
                await LoadGenres();
            }

            return _genres.FirstOrDefault(g => g.Id == genreId);
        }

        public async Task<IList<Genre>> GetAll()
        {
            if (_genres.Count == 0)
                await LoadGenres();

            return _genres;
        }

        public async Task LoadGenres()
        {
            var tasks = new[] { GetTvGenres(), GetMovieGenres() };

            var results = await Task.WhenAll(tasks);

            var allGenres = results.SelectMany(r => r);
            allGenres = allGenres.DistinctBy(g => g.Id);

            _genres = allGenres.ToList();
        }

        private async Task<IList<Genre>> GetTvGenres()
        {
            var url = $"https://api.themoviedb.org/3/genre/tv/list?api_key={_configuration.TmdbApiKey}&language=en-US";

            var response = await _cachedHttpHelper.Fetch<GenreListResponse>(_httpClient, url, TimeSpan.FromDays(10));
            if (response != null)
            {
                return response.Genres;
            }
            return new List<Genre>();

        }
        private async Task<IList<Genre>> GetMovieGenres()
        {
            var url = $"https://api.themoviedb.org/3/genre/movie/list?api_key={_configuration.TmdbApiKey}&language=en-US";

            var response = await _cachedHttpHelper.Fetch<GenreListResponse>(_httpClient, url, TimeSpan.FromDays(10));
            if (response != null)
            {
                return response.Genres;
            }
            return new List<Genre>();

        }
    }
}