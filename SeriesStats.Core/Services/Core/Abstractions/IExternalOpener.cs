using System.Threading.Tasks;

namespace SeriesStats.Core.Services.Core.Abstractions
{
    public interface IExternalOpener
    {
        Task OpenEpisodeOnTrakt(string showSlug, int seasonNumber, int episodeNumber);
        Task OpenSeasonOnTrakt(string showSlug, int seasonNumber);
        Task OpenShowOnTrakt(string showSlug);

        Task OpenEpisodeOnMovieDb(int showId, int seasonNumber, int episodeNumber);
        Task OpenSeasonOnMovieDb(int showId, int seasonNumber);
        Task OpenShowOnMovieDb(int showId);
    }
}