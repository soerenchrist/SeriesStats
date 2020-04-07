using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SeriesStats.Core.Auth.Abstractions;
using SeriesStats.Core.Models.Auth;
using SeriesStats.Core.Repository.Abstractions;
using SeriesStats.Core.Util;
using SeriesStats.Core.Util.Abstractions;

namespace SeriesStats.Core.Auth
{
    public class Authenticator : IAuthenticator
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IApiConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public Authenticator(ITokenRepository tokenRepository,
            IApiConfiguration configuration)
        {
            _tokenRepository = tokenRepository;
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        public async Task<bool> Logout()
        {
            var token = await GetAccessToken();
            var url = "https://api.trakt.tv/oauth/revoke";
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var revokeBody = new RevokeAccessTokenRequest
            {
                AccessToken = token,
                ClientId = _configuration.TraktClientId,
                ClientSecret = _configuration.TraktClientSecret
            };
            request.Content = new StringContent(JsonConvert.SerializeObject(revokeBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                _tokenRepository.RemoveAccessToken();
                return true;
            }

            return false;
        }

        public async Task<bool> ExchangeOAuthCode(string oauthCode)
        {
            var url = "https://api.trakt.tv/oauth/token";
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var content = new OAuthTokenRequest
            {
                Code = oauthCode,
                ClientId = _configuration.TraktClientId,
                ClientSecret = _configuration.TraktClientSecret,
                RedirectUri = Constants.RedirectUri,
                GrantType = "authorization_code"
            };
            request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var accessToken = JsonConvert.DeserializeObject<AccessTokenResponse>(responseContent);
                await _tokenRepository.SaveAccessToken(accessToken);
                return true;
            }

            return false;
        }

        public async Task<bool> IsAuthenticated()
        {
            var token = await _tokenRepository.GetAccessToken();
            if (token == null) return false;
            return token.IsValid;
        }
        public async Task<string> GetAccessToken()
        {
            var token = await _tokenRepository.GetAccessToken();
            if (token == null) return null;
            if (!token.IsValid)
            {
                token = await RefreshToken(token);
                if (token == null)
                {
                    return null;
                }
            }
            return token?.AccessToken;
        }

        private async Task<AccessTokenResponse> RefreshToken(AccessTokenResponse token)
        {
            var url = "https://api.trakt.tv/oauth/token";
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var content = new RefreshTokenRequest
            {
                RefreshToken = token.RefreshToken,
                ClientId = _configuration.TraktClientId,
                ClientSecret = _configuration.TraktClientSecret,
                RedirectUri = Constants.RedirectUri,
                GrantType = "authorization_code"
            };
            request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var accessToken = JsonConvert.DeserializeObject<AccessTokenResponse>(responseContent);
                await _tokenRepository.SaveAccessToken(accessToken);
                return accessToken;
            }

            return null;
        }
    }
}