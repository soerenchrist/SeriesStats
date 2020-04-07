using Newtonsoft.Json;
using System.Collections.Generic;

namespace SeriesStats.Core.Models.Trakt.Checkin
{
    public class CheckinRequest
    {
        [JsonProperty("episodes")]
        public IList<CheckinEpisode> Episodes { get; set; }
    }
}