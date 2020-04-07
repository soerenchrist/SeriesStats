using SeriesStats.Core.Models.Trakt.Base;
using System.Threading.Tasks;

namespace SeriesStats.Core.Services.MovieDb.Abstractions
{
    public interface IImageService
    {
        Task<TraktImage> GetImageFor(int showId);
    }
}