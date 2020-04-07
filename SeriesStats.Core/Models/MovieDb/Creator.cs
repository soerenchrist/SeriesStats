using Newtonsoft.Json;

namespace SeriesStats.Core.Models.MovieDb
{
    public class Creator
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("credit_id")]
        public string CreditId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public long Gender { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }
    }
}