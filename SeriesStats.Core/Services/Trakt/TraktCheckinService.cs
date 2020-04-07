using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SeriesStats.Core.Auth.Abstractions;
using SeriesStats.Core.Models.Trakt;
using SeriesStats.Core.Models.Trakt.Checkin;
using SeriesStats.Core.Services.Trakt.Abstractions;
using SeriesStats.Core.Util;
using SeriesStats.Core.Util.Abstractions;

namespace SeriesStats.Core.Services.Trakt
{
    public class TraktCheckinService : ITraktCheckinService
    {
        private readonly IAuthenticator _authenticator;
        private readonly HttpClient _httpClient;
        
        public TraktCheckinService(IAuthenticator authenticator,
            IApiConfiguration configuration)
        {
            _authenticator = authenticator;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("trakt-api-version", "2");
            _httpClient.DefaultRequestHeaders.Add("trakt-api-key", configuration.TraktClientId);
        }

        public async Task<bool> CheckinEpisode(TraktEpisodeHeader episode, DateTimeOffset watchedAt)
        {
            var accessToken = await _authenticator.GetAccessToken();
            if (accessToken == null) return false;
            var request = new CheckinRequest
            {
                Episodes = new List<CheckinEpisode>
                {
                    new CheckinEpisode
                    {
                        WatchedAt = watchedAt,
                        Ids = episode.Ids
                    }
                }
            };
            var url = "https://api.trakt.tv/sync/history";
            var message = new HttpRequestMessage(HttpMethod.Post, url);
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            message.Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(message);
            return response.IsSuccessStatusCode;
        }
    }
}