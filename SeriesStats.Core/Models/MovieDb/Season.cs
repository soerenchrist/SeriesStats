using Newtonsoft.Json;
using SeriesStats.Core.Models.MovieDb.Shows;
using System;
using System.Collections.Generic;

namespace SeriesStats.Core.Models.MovieDb
{
    public class Season
    {
        [JsonProperty("air_date")]
        public DateTimeOffset AirDate { get; set; }

        [JsonProperty("episode_count")]
        public long EpisodeCount { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("season_number")]
        public int SeasonNumber { get; set; }
        [JsonProperty("episodes")]
        public List<MovieDbEpisode> Episodes { get; set; }
    }
}