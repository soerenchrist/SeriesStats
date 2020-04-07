using Newtonsoft.Json;
using System.Collections.Generic;

namespace SeriesStats.Core.Models.Trakt
{
    public class TraktSeasonHeader
    {
        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("episodes")]
        public List<TraktEpisodeHeader> Episodes { get; set; }
    }

}