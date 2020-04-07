using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SeriesStats.Core.Auth.Abstractions;
using SeriesStats.Core.Models.Trakt.User;
using SeriesStats.Core.Services.Trakt.Abstractions;
using SeriesStats.Core.Util.Abstractions;

namespace SeriesStats.Core.Services.Trakt
{
    public class TraktUserService : ITraktUserService
    {
        private readonly IAuthenticator _authenticator;
        private readonly ICachedHttpHelper _cachedHttpHelper;
        private readonly HttpClient _httpClient;

        public TraktUserService(IAuthenticator authenticator,
            ICachedHttpHelper cachedHttpHelper,
            IApiConfiguration configuration)
        {

            _authenticator = authenticator;
            _cachedHttpHelper = cachedHttpHelper;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("trakt-api-version", "2");
            _httpClient.DefaultRequestHeaders.Add("trakt-api-key", configuration.TmdbApiKey);
        }

        public async Task<TraktUser> GetTraktUser()
        {
            var url = "https://api.trakt.tv/users/settings";
            var accessToken = await _authenticator.GetAccessToken();
            if (accessToken == null) return null;
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var settings = await _cachedHttpHelper.Fetch<TraktUserSettings>(_httpClient, request, TimeSpan.FromDays(1));
            return settings?.User;
        }
    }
}