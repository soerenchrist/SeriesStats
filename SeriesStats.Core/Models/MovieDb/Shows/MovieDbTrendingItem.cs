using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SeriesStats.Core.Models.MovieDb.Shows
{
    public class MovieDbTrendingItem
    {
        [JsonProperty("original_name")]
        public string OriginalName { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("vote_count")]
        public long VoteCount { get; set; }

        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }

        [JsonProperty("first_air_date")]
        public DateTimeOffset? FirstAirDate { get; set; }
        [JsonIgnore]
        public string FirstAirYear => FirstAirDate.HasValue ? FirstAirDate.Value.Year.ToString() : "";

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("genre_ids")]
        public List<int> GenreIds { get; set; }
        [JsonIgnore]
        public List<Genre> Genres { get; set; }

        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("origin_country")]
        public List<string> OriginCountry { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

    }
}