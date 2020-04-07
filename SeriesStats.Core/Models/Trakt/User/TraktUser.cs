using Newtonsoft.Json;

namespace SeriesStats.Core.Models.Trakt.User
{
    public class TraktUser
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("images")]
        public TraktUserImages Images { get; set; }
    }
}