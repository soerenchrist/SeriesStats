using Newtonsoft.Json;
using System.Collections.Generic;

namespace SeriesStats.Core.Models.MovieDb.Shows
{
    public class MovieDbTrendingItemResponse
    {
        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("results")]
        public List<MovieDbTrendingItem> Results { get; set; }

        [JsonProperty("total_pages")]
        public long TotalPages { get; set; }

        [JsonProperty("total_results")]
        public long TotalResults { get; set; }
    }
}
