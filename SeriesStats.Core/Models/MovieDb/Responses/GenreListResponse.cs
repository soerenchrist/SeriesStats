using Newtonsoft.Json;
using System.Collections.Generic;

namespace SeriesStats.Core.Models.MovieDb.Responses
{
    public class GenreListResponse
    {
        [JsonProperty("genres")]
        public IList<Genre> Genres { get; set; }
    }
}