using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SeriesStats.Core.Models.Trakt
{
    public class TraktLastWatchedShow
    {
        [JsonProperty("plays")]
        public int Plays { get; set; }

        [JsonProperty("last_watched_at")]
        public DateTimeOffset LastWatchedAt { get; set; }

        [JsonProperty("last_updated_at")]
        public DateTimeOffset LastUpdatedAt { get; set; }

        [JsonProperty("reset_at")]
        public object ResetAt { get; set; }

        [JsonProperty("show")]
        public TraktShow Show { get; set; }

        [JsonProperty("seasons")]
        public List<TraktSeasonHeader> Seasons { get; set; }
    }
}