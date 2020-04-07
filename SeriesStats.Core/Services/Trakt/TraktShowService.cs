using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SeriesStats.Core.Auth.Abstractions;
using SeriesStats.Core.Models.Core;
using SeriesStats.Core.Models.Trakt;
using SeriesStats.Core.Services.Core.Abstractions;
using SeriesStats.Core.Services.MovieDb.Abstractions;
using SeriesStats.Core.Services.Trakt.Abstractions;
using SeriesStats.Core.Util;
using SeriesStats.Core.Util.Abstractions;

namespace SeriesStats.Core.Services.Trakt
{
    public class TraktShowService : ITraktShowService
    {
        private readonly IAuthenticator _authenticator;
        private readonly ICachedHttpHelper _cachedHttpHelper;
        private readonly IImageService _imageService;
        private readonly IShowFilterer _showFilterer;
        private readonly HttpClient _httpClient;

        public TraktShowService(IAuthenticator authenticator,
            ICachedHttpHelper cachedHttpHelper,
            IImageService imageService,
            IShowFilterer showFilterer,
            IApiConfiguration configuration)
        {
            _authenticator = authenticator;
            _cachedHttpHelper = cachedHttpHelper;
            _imageService = imageService;
            _showFilterer = showFilterer;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("trakt-api-version", "2");
            _httpClient.DefaultRequestHeaders.Add("trakt-api-key", configuration.TraktClientId);
        }

        public async Task<IList<TraktLastWatchedShow>> GetWatchedShows(bool forceRefresh = false)
        {
            var url = "https://api.trakt.tv/sync/watched/shows?extended=full";
            var accessToken = await _authenticator.GetAccessToken();
            if (accessToken == null) return new List<TraktLastWatchedShow>();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return await _cachedHttpHelper.Fetch<IList<TraktLastWatchedShow>>(_httpClient, request, TimeSpan.FromHours(3), forceRefresh);
        }

        public async IAsyncEnumerable<TraktShow> GetMyShows(FilterOptions filterOptions, SortOptions sortOptions = SortOptions.ByName, bool forceRefresh = false)
        {
            var watches = await GetWatchedShows(forceRefresh);
            switch (sortOptions)
            {
                case SortOptions.ByName:
                    watches = watches.OrderBy(w => w.Show.Title).ToList();
                    break;
                case SortOptions.ByLastViewed:
                    break;
                case SortOptions.ByNewest:
                    watches = watches.OrderBy(w => w.LastUpdatedAt).ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sortOptions), sortOptions, null);
            }
            var shows = watches.Select(w => w.Show).ToList();
            foreach (var show in shows)
            {
                if (!_showFilterer.FilterApplies(show, filterOptions))
                    continue;

                if (show.Ids.Tmdb.HasValue)
                {
                    show.Image = await _imageService.GetImageFor(show.Ids.Tmdb.Value);
                    show.ShowProgress = await GetShowProgress(show.Ids.Trakt, forceRefresh);
                }

                yield return show;
            }
        }

        public async Task<TraktShowProgress> GetShowProgress(int traktId, bool forceRefresh = false)
        {
            var url = $"https://api.trakt.tv/shows/{traktId}/progress/watched";
            var accessToken = await _authenticator.GetAccessToken();
            if (accessToken == null) return null;

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var result = await _cachedHttpHelper.Fetch<TraktShowProgress>(_httpClient, request, TimeSpan.FromHours(1), forceRefresh);
            if (result == null) return null;
            result.ShowId = traktId;

            return result;
        }
    }
}