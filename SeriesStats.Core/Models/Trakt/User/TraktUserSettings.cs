

using Newtonsoft.Json;

namespace SeriesStats.Core.Models.Trakt.User
{
    public class TraktUserSettings
    {
        [JsonProperty("user")]
        public TraktUser User { get; set; }
    }
}