using Newtonsoft.Json;
using System.Collections.Generic;

namespace SeriesStats.Core.Models.MovieDb.Image
{
    public class ShowImages
    {
        [JsonProperty("backdrops")]
        public List<Image> Backdrops { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("posters")]
        public List<Image> Posters { get; set; }
    }
}