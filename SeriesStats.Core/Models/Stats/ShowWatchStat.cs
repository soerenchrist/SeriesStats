using SeriesStats.Core.Models.Trakt;

namespace SeriesStats.Core.Models.Stats
{
    public class ShowWatchStat
    {
        public TraktShow Show { get; set; }
        public int MinutesWatched { get; set; }
        public int NumberOfWatches { get; set; }
    }
}