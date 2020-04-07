
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SeriesStats.Core.Models.MovieDb.Shows
{

    public class MovieDbShowDetail
    {
        [JsonProperty("backdrop_path")]
        public object BackdropPath { get; set; }

        [JsonProperty("created_by")]
        public List<Creator> CreatedBy { get; set; }

        [JsonProperty("episode_run_time")]
        public List<long> EpisodeRunTime { get; set; }

        [JsonProperty("first_air_date")]
        public DateTimeOffset FirstAirDate { get; set; }

        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; }

        [JsonProperty("homepage")]
        public Uri Homepage { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("in_production")]
        public bool InProduction { get; set; }

        [JsonProperty("languages")]
        public List<string> Languages { get; set; }

        [JsonProperty("last_air_date")]
        public DateTimeOffset LastAirDate { get; set; }

        [JsonProperty("last_episode_to_air")]
        public LastEpisodeToAir LastEpisodeToAir { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("next_episode_to_air")]
        public object NextEpisodeToAir { get; set; }

        [JsonProperty("networks")]
        public List<Network> Networks { get; set; }

        [JsonProperty("number_of_episodes")]
        public long NumberOfEpisodes { get; set; }

        [JsonProperty("number_of_seasons")]
        public long NumberOfSeasons { get; set; }

        [JsonProperty("origin_country")]
        public List<string> OriginCountry { get; set; }

        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonProperty("original_name")]
        public string OriginalName { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("production_companies")]
        public List<object> ProductionCompanies { get; set; }

        [JsonProperty("seasons")]
        public List<Season> Seasons { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }

        [JsonProperty("vote_count")]
        public long VoteCount { get; set; }
    }
}
