using SeriesStats.Core.Models.Auth;
using System.Threading.Tasks;

namespace SeriesStats.Core.Repository.Abstractions
{
    public interface ITokenRepository
    {
        Task SaveAccessToken(AccessTokenResponse accessToken);
        Task<AccessTokenResponse> GetAccessToken();
        void RemoveAccessToken();
    }
}