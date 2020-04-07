using SeriesStats.Core.Models.Stats;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriesStats.Core.Services.Trakt.Abstractions
{
    public interface ITraktStatsService
    {
        Task<BaseStats> GetStats(bool forceRefresh = false);
        Task<IList<ByGenre>> GetGenreStats(int top = 10, bool forceRefresh = false);
        Task<IList<ByWeekday>> GetWeekdayStats(bool forceRefresh = false);
        Task<IList<HistoryItem>> GetHistory(HistoryMode historyMode, bool forceRefresh = false);
        Task<IList<ByTimeOfDay>> GetTimeOfDayStats(bool forceRefresh = false);
        Task<IList<ShowWatchStat>> GetMostWatchedShows(int top = 5, StatDisplayMode mode = StatDisplayMode.Minutes, bool forceRefresh = false);
    }
}