using Newtonsoft.Json;

namespace SeriesStats.Core.Models.MovieDb
{
    public class Cast
    {
        [JsonProperty("character")]
        public string Character { get; set; }
        [JsonProperty("credit_id")]
        public string CreditId { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("gender")]
        public int Gender { get; set; }
        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }
        [JsonProperty("order")]
        public int Order { get; set; }
    }
}