using Newtonsoft.Json;

namespace SeriesStats.Core.Models.MovieDb.Movies
{
    public class ProductionCountry
    {
        [JsonProperty("iso_3166_1")]
        public string Language { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}