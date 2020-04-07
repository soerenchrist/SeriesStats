using System.Threading.Tasks;

namespace SeriesStats.Core.Auth.Abstractions
{
    public interface IAuthenticator
    {
        Task<bool> IsAuthenticated();
        Task<string> GetAccessToken();
        Task<bool> Logout();
        Task<bool> ExchangeOAuthCode(string oauthCode);
    }
}