using SeriesStats.Core.Models.Trakt;
using System;
using System.Threading.Tasks;

namespace SeriesStats.Core.Services.Trakt.Abstractions
{
    public interface ITraktCheckinService
    {
        Task<bool> CheckinEpisode(TraktEpisodeHeader episode, DateTimeOffset watchedAt);
    }
}