using Newtonsoft.Json;

namespace SeriesStats.Core.Models.Auth
{
    public class AccessTokenRequest
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }
    }
}