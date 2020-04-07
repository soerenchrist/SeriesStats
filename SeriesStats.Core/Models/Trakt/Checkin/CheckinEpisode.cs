using Newtonsoft.Json;
using SeriesStats.Core.Models.Trakt.Base;
using System;

namespace SeriesStats.Core.Models.Trakt.Checkin
{
    public class CheckinEpisode
    {
        [JsonProperty("watched_at")]
        public DateTimeOffset WatchedAt { get; set; }
        [JsonProperty("ids")]
        public TraktIds Ids { get; set; }
    }
}