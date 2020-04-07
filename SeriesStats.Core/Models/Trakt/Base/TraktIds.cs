using Newtonsoft.Json;

namespace SeriesStats.Core.Models.Trakt.Base
{
    public class TraktIds
    {
        [JsonProperty("trakt")]
        public int Trakt { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("tvdb")]
        public long Tvdb { get; set; }

        [JsonProperty("imdb")]
        public string Imdb { get; set; }

        [JsonProperty("tmdb")]
        public int? Tmdb { get; set; }

        [JsonProperty("tvrage")]
        public long? Tvrage { get; set; }
    }
}