using System;

namespace SeriesStats.Core.Models.Stats
{
    public class HistoryItem
    {
        public DateTime DateTime { get; set; }
        public int NumberOfPlays { get; set; }
        public int MinutesPlayed { get; set; }
    }
}