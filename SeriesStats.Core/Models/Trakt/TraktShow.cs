using Newtonsoft.Json;
using SeriesStats.Core.Models.Trakt.Base;
using System.Collections.Generic;

namespace SeriesStats.Core.Models.Trakt
{
    public class TraktShow
    {
        [JsonIgnore] public int Id => Ids.Trakt;
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("year")]
        public long Year { get; set; }

        [JsonProperty("ids")]
        public TraktIds Ids { get; set; }

        [JsonProperty("genres")]
        public List<string> Genres { get; set; }
        [JsonProperty("runtime")]
        public int Runtime { get; set; }
        [JsonIgnore] public TraktImage Image { get; set; }
        [JsonIgnore] public TraktShowProgress ShowProgress { get; set; }
        [JsonIgnore] public bool IsFavorite { get; set; }
    }
}