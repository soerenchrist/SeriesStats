using Humanizer;
using SeriesStats.Core.Models.Stats;
using SeriesStats.Core.Models.Trakt;
using SeriesStats.Core.Services.Trakt.Abstractions;
using SeriesStats.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeriesStats.Core.Services.Trakt
{
    public class TraktStatsService : ITraktStatsService
    {
        private readonly ITraktShowService _showService;

        public TraktStatsService(ITraktShowService showService)
        {
            _showService = showService;
        }

        public async Task<BaseStats> GetStats(bool forceRefresh = false)
        {
            var weekdayStats = await GetWeekdayStats(forceRefresh);
            return new BaseStats
            {
                EpisodeCount = weekdayStats.Sum(w => w.NumberOfPlays),
                TotalMinutes = weekdayStats.Sum(w => w.MinutesPlayed)
            };
        }

        public async Task<IList<ByGenre>> GetGenreStats(int top = 10, bool forceRefresh = false)
        {
            var results = new List<ByGenre>();
            var watches = await _showService.GetWatchedShows(forceRefresh);

            foreach (var watch in watches)
            {
                var show = watch.Show;
                var genres = show.Genres ?? new List<string>();

                foreach (var genre in genres)
                {
                    var genreName = genre.Humanize();
                    var byGenre = results.FirstOrDefault(r => r.Genre == genreName);
                    if (byGenre == null)
                    {
                        byGenre = new ByGenre();
                        byGenre.Genre = genreName;
                        byGenre.NumberOfPlays = watch.Plays;
                        byGenre.MinutesPlayed = show.Runtime * watch.Plays;
                        results.Add(byGenre);
                    }
                    else
                    {
                        byGenre.NumberOfPlays += watch.Plays;
                        byGenre.MinutesPlayed += show.Runtime * watch.Plays;
                    }
                }
            }
            return top == -1 ? results.ToList() : results.Take(top).ToList();
        }

        public async Task<IList<ByWeekday>> GetWeekdayStats(bool forceRefresh = false)
        {
            var results = new List<ByWeekday>
            {
                new ByWeekday {Weekday = DayOfWeek.Monday.ToString()},
                new ByWeekday {Weekday = DayOfWeek.Tuesday.ToString()},
                new ByWeekday {Weekday = DayOfWeek.Wednesday.ToString()},
                new ByWeekday {Weekday = DayOfWeek.Thursday.ToString()},
                new ByWeekday {Weekday = DayOfWeek.Friday.ToString()},
                new ByWeekday {Weekday = DayOfWeek.Saturday.ToString()},
                new ByWeekday {Weekday = DayOfWeek.Sunday.ToString()}
            };
            var watches = await _showService.GetWatchedShows(forceRefresh);

            foreach (var watch in watches)
            {
                var show = watch.Show;

                foreach (var season in watch.Seasons)
                {
                    foreach (var episode in season.Episodes)
                    {
                        var weekday = episode.LastWatchedAt.DayOfWeek.ToString();
                        var byWeekday = results.FirstOrDefault(r => r.Weekday == weekday);
                        if (byWeekday != null)
                        {
                            byWeekday.NumberOfPlays++;
                            byWeekday.MinutesPlayed += show.Runtime;
                        }
                    }
                }
            }

            return results.ToList();
        }

        public async Task<IList<HistoryItem>> GetHistory(HistoryMode historyMode, bool forceRefresh = false)
        {
            var watches = await _showService.GetWatchedShows(forceRefresh);
            switch (historyMode)
            {
                case HistoryMode.LastWeek:
                    return GetLastWeekHistory(watches);
                case HistoryMode.LastMonth:
                    return GetLastMonthHistory(watches);
                case HistoryMode.LastYear:
                    return GetLastYearHistory(watches);
                default:
                    return GetLastWeekHistory(watches);
            }
        }

        public async Task<IList<ByTimeOfDay>> GetTimeOfDayStats(bool forceRefresh = false)
        {
            var watches = await _showService.GetWatchedShows(forceRefresh);
            var results = new List<ByTimeOfDay>();
            for (int i = 0; i < 24; i++)
            {
                results.Add(new ByTimeOfDay
                {
                    TimeOfDay = i.ToString()
                });
            }

            var allEpisodes = GetAllEpisodes(watches);
            foreach (var episode in allEpisodes)
            {
                var hour = episode.LastWatchedAt.Hour;
                var result = results.FirstOrDefault(r => r.TimeOfDay == hour.ToString());
                if (result != null)
                {
                    result.MinutesPlayed += episode.Show.Runtime;
                    result.NumberOfPlays++;
                }
            }

            return results;
        }

        public async Task<IList<ShowWatchStat>> GetMostWatchedShows(int top = 5, StatDisplayMode mode = StatDisplayMode.Minutes, bool forceRefresh = false)
        {
            var watches = await _showService.GetWatchedShows(forceRefresh);
            var shows = new List<ShowWatchStat>();
            foreach (var watch in watches)
            {
                var show = shows.FirstOrDefault(s => s.Show.Ids.Trakt == watch.Show.Ids.Trakt);
                if (show == null)
                {
                    show = new ShowWatchStat
                    {
                        Show = watch.Show,
                        MinutesWatched = watch.Plays * watch.Show.Runtime,
                        NumberOfWatches = watch.Plays
                    };
                    shows.Add(show);
                }
            }

            switch (mode)
            {
                case StatDisplayMode.Plays:
                    shows = shows.OrderByDescending(s => s.NumberOfWatches).ToList();
                    break;
                case StatDisplayMode.Minutes:
                    shows = shows.OrderByDescending(s => s.MinutesWatched).ToList();
                    break;
            }

            return top == -1 ? shows : shows.Take(top).ToList();
        }

        private IList<HistoryItem> GetLastWeekHistory(IList<TraktLastWatchedShow> watches)
        {
            var today = DateTime.Now.Date;
            var currentDay = today.AddDays(-7);

            var allEpisodes = GetAllEpisodes(watches);

            var results = new List<HistoryItem>();
            while (true)
            {
                if (currentDay.Equals(today.AddDays(1)))
                {
                    break;
                }

                var day = currentDay;
                var episodesOnDay = allEpisodes.Where(e => e.LastWatchedAt.Date.Equals(day)).ToList();

                var sum = 0;
                foreach (var ep in episodesOnDay)
                {
                    sum += ep.Show.Runtime;
                }

                results.Add(new HistoryItem
                {
                    DateTime = day,
                    NumberOfPlays = episodesOnDay.Count(),
                    MinutesPlayed = sum
                });

                currentDay = currentDay.AddDays(1);
            }

            return results;
        }

        private IList<HistoryItem> GetLastMonthHistory(IList<TraktLastWatchedShow> watches)
        {
            var today = DateTime.Now.Date;
            var currentDay = today.AddDays(-35);

            var allEpisodes = GetAllEpisodes(watches);

            var results = new List<HistoryItem>();
            while (true)
            {
                if (currentDay.Equals(today.AddDays(7)))
                {
                    break;
                }

                var day = currentDay;
                var startOfWeek = day.StartOfWeek(DayOfWeek.Monday);
                var endOfWeek = startOfWeek.AddDays(7);
                var episodesInWeek = allEpisodes.Where(e =>
                    e.LastWatchedAt.Date.Date.Between(startOfWeek, endOfWeek)).ToList();

                var sum = 0;
                foreach (var ep in episodesInWeek)
                {
                    sum += ep.Show.Runtime;
                }

                results.Add(new HistoryItem
                {
                    DateTime = day,
                    NumberOfPlays = episodesInWeek.Count(),
                    MinutesPlayed = sum
                });

                currentDay = currentDay.AddDays(7);
            }

            return results;
        }

        private IList<HistoryItem> GetLastYearHistory(IList<TraktLastWatchedShow> watches)
        {
            var today = DateTime.Now.Date;
            var currentDay = today.AddDays(-360);

            var allEpisodes = GetAllEpisodes(watches);

            var results = new List<HistoryItem>();
            while (true)
            {
                if (currentDay.Equals(today.AddDays(30)))
                {
                    break;
                }

                var day = currentDay;
                var firstDayOfMonth = new DateTime(day.Year, day.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                var episodesInMonth = allEpisodes.Where(e =>
                    e.LastWatchedAt.DateTime.AtMidnight().AddHours(1).Between(firstDayOfMonth, lastDayOfMonth)).ToList();

                var sum = 0;
                foreach (var ep in episodesInMonth)
                {
                    sum += ep.Show.Runtime;
                }

                results.Add(new HistoryItem
                {
                    DateTime = day,
                    NumberOfPlays = episodesInMonth.Count(),
                    MinutesPlayed = sum
                });

                currentDay = currentDay.AddDays(30);
            }

            return results;
        }

        private List<TraktEpisodeHeader> GetAllEpisodes(IList<TraktLastWatchedShow> watches)
        {
            var allEpisodes = new List<TraktEpisodeHeader>();
            foreach (var watch in watches)
            {
                foreach (var season in watch.Seasons)
                {
                    foreach (var episode in season.Episodes)
                    {
                        episode.Show = watch.Show;
                        allEpisodes.Add(episode);
                    }
                }
            }

            return allEpisodes;
        }
    }
}