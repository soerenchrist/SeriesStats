using Newtonsoft.Json;

namespace SeriesStats.Core.Models.Trakt.User
{
    public class TraktUserImages
    {
        [JsonProperty("avatar")]
        public Avatar Avatar { get; set; }
    }

    public class Avatar
    {
        [JsonProperty("full")]
        public string FullPath { get; set; }
    }

}