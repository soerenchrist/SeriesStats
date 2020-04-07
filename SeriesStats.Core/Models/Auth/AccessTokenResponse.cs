﻿using Newtonsoft.Json;
using System;

namespace SeriesStats.Core.Models.Auth
{
    public class AccessTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        public bool IsValid => DateTimeOffset.UtcNow.ToUnixTimeSeconds() - CreatedAt < ExpiresIn;

    }
}