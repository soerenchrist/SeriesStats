using Newtonsoft.Json;

namespace SeriesStats.Core.Models.Trakt
{
    public class TraktShowProgress
    {
        [JsonIgnore]
        public int ShowId { get; set; }
        [JsonProperty("aired")]
        public int Aired { get; set; }
        [JsonProperty("completed")]
        public int Completed { get; set; }
        [JsonProperty("next_episode")]
        public TraktEpisodeHeader NextEpisode { get; set; }
    }
}