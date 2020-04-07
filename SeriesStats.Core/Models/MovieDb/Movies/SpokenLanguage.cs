using Newtonsoft.Json;

namespace SeriesStats.Core.Models.MovieDb.Movies
{
    public class SpokenLanguage
    {
        [JsonProperty("iso_639_1")]
        public string Iso639_1 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}