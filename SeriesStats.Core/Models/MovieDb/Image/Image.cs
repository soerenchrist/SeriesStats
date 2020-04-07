using Newtonsoft.Json;

namespace SeriesStats.Core.Models.MovieDb.Image
{
    public class Image
    {
        [JsonProperty("aspect_ratio")]
        public double AspectRatio { get; set; }
        [JsonProperty("file_path")]
        public string FilePath { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("iso_639_1")]
        public string Language { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
    }
}