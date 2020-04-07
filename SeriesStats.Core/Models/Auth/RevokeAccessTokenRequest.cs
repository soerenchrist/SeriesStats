using Newtonsoft.Json;

namespace SeriesStats.Core.Models.Auth
{
    public class RevokeAccessTokenRequest
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }
        [JsonProperty("token")]
        public string AccessToken { get; set; }
    }
}