using SeriesStats.Core.Services.Core.Abstractions;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SeriesStats.Util
{
    public class ExternalOpener : IExternalOpener
    {
        public async Task OpenEpisodeOnTrakt(string showSlug, int seasonNumber, int episodeNumber)
        {
            await Browser.OpenAsync(
                $"https://trakt.tv/shows/{showSlug}/seasons/{seasonNumber}/episodes/{episodeNumber}");
        }

        public async Task OpenSeasonOnTrakt(string showSlug, int seasonNumber)
        {
            await Browser.OpenAsync(
                $"https://trakt.tv/shows/{showSlug}/seasons/{seasonNumber}");
        }

        public async Task OpenShowOnTrakt(string showSlug)
        {
            await Browser.OpenAsync(
                $"https://trakt.tv/shows/{showSlug}");
        }

        public async Task OpenEpisodeOnMovieDb(int showId, int seasonNumber, int episodeNumber)
        {
            await Browser.OpenAsync(
                $"https://www.themoviedb.org/tv/{showId}/season/{seasonNumber}/episode/{episodeNumber}");
        }

        public async Task OpenSeasonOnMovieDb(int showId, int seasonNumber)
        {
            await Browser.OpenAsync(
                $"https://www.themoviedb.org/tv/{showId}/season/{seasonNumber}");
        }

        public async Task OpenShowOnMovieDb(int showId)
        {
            await Browser.OpenAsync(
                $"https://www.themoviedb.org/tv/{showId}");
        }
    }
}