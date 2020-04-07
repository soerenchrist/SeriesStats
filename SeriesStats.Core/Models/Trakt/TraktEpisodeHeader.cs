using Newtonsoft.Json;
using SeriesStats.Core.Models.Trakt.Base;
using System;

namespace SeriesStats.Core.Models.Trakt
{
    public class TraktEpisodeHeader
    {
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("plays")]
        public int Plays { get; set; }

        [JsonProperty("last_watched_at")]
        public DateTimeOffset LastWatchedAt { get; set; }
        [JsonProperty("season")] public int Season { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("ids")] public TraktIds Ids { get; set; }


        [JsonIgnore] public TraktShow Show { get; set; }
    }
}