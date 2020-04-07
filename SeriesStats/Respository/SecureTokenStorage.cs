using SeriesStats.Core.Models.Auth;
using SeriesStats.Core.Repository.Abstractions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SeriesStats.Respository
{
    public class SecureTokenStorage : ITokenRepository
    {
        private const string _tokenKey = "oauth_token";
        private const string _createdAtKey = "oauth_created_at";
        private const string _expiresInKey = "oauth_expires_in";
        private const string _refreshKey = "oauth_refresh_token";

        public async Task SaveAccessToken(AccessTokenResponse accessToken)
        {
            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                Preferences.Set(_tokenKey, accessToken.AccessToken);
                Preferences.Set(_createdAtKey, accessToken.CreatedAt.ToString());
                Preferences.Set(_expiresInKey, accessToken.ExpiresIn.ToString()); 
                Preferences.Set(_refreshKey, accessToken.RefreshToken);
            }
            else
            {
                await SecureStorage.SetAsync(_tokenKey, accessToken.AccessToken);
                await SecureStorage.SetAsync(_createdAtKey, accessToken.CreatedAt.ToString());
                await SecureStorage.SetAsync(_expiresInKey, accessToken.ExpiresIn.ToString());
                await SecureStorage.SetAsync(_refreshKey, accessToken.RefreshToken);
            }
        }

        public async Task<AccessTokenResponse> GetAccessToken()
        {
            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                var token = Preferences.Get(_tokenKey, "");
                if (string.IsNullOrWhiteSpace(token))
                {
                    return null;
                }

                var createdAt = Preferences.Get(_createdAtKey, 0);
                var expiresIn = Preferences.Get(_expiresInKey, 0);
                var refreshToken = Preferences.Get(_refreshKey, "");
                return new AccessTokenResponse
                {
                    AccessToken = token,
                    CreatedAt = createdAt,
                    ExpiresIn = expiresIn,
                    RefreshToken = refreshToken
                };
            }
            else
            {
                var token = await SecureStorage.GetAsync(_tokenKey);
                if (string.IsNullOrWhiteSpace(token))
                {
                    return null;
                }

                var createdAt = long.Parse(await SecureStorage.GetAsync(_createdAtKey));
                var expiresIn = int.Parse(await SecureStorage.GetAsync(_expiresInKey));
                var refreshToken = await SecureStorage.GetAsync(_refreshKey);
                return new AccessTokenResponse
                {
                    AccessToken = token,
                    CreatedAt = createdAt,
                    ExpiresIn = expiresIn,
                    RefreshToken = refreshToken
                };
            }
        }

        public void RemoveAccessToken()
        {
            SecureStorage.RemoveAll();
        }
    }
}