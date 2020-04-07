using Newtonsoft.Json;
using System;

namespace SeriesStats.Core.Models.MovieDb
{
    public class LastEpisodeToAir
    {
        [JsonProperty("air_date")]
        public DateTimeOffset AirDate { get; set; }

        [JsonProperty("episode_number")]
        public long EpisodeNumber { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("production_code")]
        public string ProductionCode { get; set; }

        [JsonProperty("season_number")]
        public long SeasonNumber { get; set; }

        [JsonProperty("show_id")]
        public long ShowId { get; set; }

        [JsonProperty("still_path")]
        public object StillPath { get; set; }

        [JsonProperty("vote_average")]
        public long VoteAverage { get; set; }

        [JsonProperty("vote_count")]
        public long VoteCount { get; set; }
    }
}