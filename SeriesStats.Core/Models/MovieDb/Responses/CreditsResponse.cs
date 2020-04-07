using Newtonsoft.Json;
using System.Collections.Generic;

namespace SeriesStats.Core.Models.MovieDb.Responses
{
    public class CreditsResponse
    {
        [JsonProperty("cast")]
        public IList<Cast> Cast { get; set; }
    }
}