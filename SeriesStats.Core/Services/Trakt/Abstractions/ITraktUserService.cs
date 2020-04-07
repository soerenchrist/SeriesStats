using SeriesStats.Core.Models.Trakt.User;
using System.Threading.Tasks;

namespace SeriesStats.Core.Services.Trakt.Abstractions
{
    public interface ITraktUserService
    {
        Task<TraktUser> GetTraktUser();
    }
}