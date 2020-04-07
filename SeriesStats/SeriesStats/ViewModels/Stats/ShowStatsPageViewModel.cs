using Microcharts;
using Prism.Commands;
using Prism.Navigation;
using SeriesStats.Core.Models.Stats;
using SeriesStats.Core.Models.Trakt.Base;
using SeriesStats.Core.Services.MovieDb.Abstractions;
using SeriesStats.Core.Services.Trakt.Abstractions;
using SeriesStats.Core.Util;
using SeriesStats.Util;
using SeriesStats.ViewModels.Base;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Entry = Microcharts.Entry;
using ValueChangedEventArgs = SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs;

namespace SeriesStats.ViewModels.Stats
{
    public class ShowStatsPageViewModel : ViewModelBase
    {
        private readonly ITraktStatsService _traktStatsService;
        private readonly IImageService _imageService;

        public BaseStats BaseStats { get; set; }
        public DonutChart GenreChart { get; set; }
        public StatDisplayMode DisplayMode { get; set; } = StatDisplayMode.Plays;
        public HistoryMode HistoryMode { get; set; } = HistoryMode.LastWeek;

        public LineChart WeekdayChart { get; set; }
        public LineChart TimeOfDayChart { get; set; }

        public LineChart HistoryChart { get; set; }

        public IList<ShowWatchStat> MostWatchedShows { get; set; }

        public ICommand DisplayModeChangedCommand { get; }
        public ICommand HistoryModeChangedCommand { get; }
        public ICommand RefreshCommand { get; }


        public ShowStatsPageViewModel(INavigationService navigationService,
            ITraktStatsService traktStatsService,
            IImageService imageService) : base(navigationService)
        {
            _traktStatsService = traktStatsService;
            _imageService = imageService;

            DisplayModeChangedCommand = new DelegateCommand<ValueChangedEventArgs>(DisplayModeChanged);
            HistoryModeChangedCommand = new DelegateCommand<ValueChangedEventArgs>(HistoryModeChanged);
            RefreshCommand = new DelegateCommand(Refresh);
        }

        private async void Refresh()
        {
            IsRefreshing = true;
            await BuildAllCharts(true);
            IsRefreshing = false;
        }

        private async void DisplayModeChanged(ValueChangedEventArgs args)
        {
            var mode = (StatDisplayMode)args.NewValue;
            if (DisplayMode == mode) return;
            DisplayMode = mode;
            await BuildAllCharts();
        }

        private async void HistoryModeChanged(ValueChangedEventArgs args)
        {
            var mode = (HistoryMode)args.NewValue;
            if (HistoryMode == mode) return;
            HistoryMode = mode;
            await BuildHistoryChart();
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await BuildAllCharts();
        }

        private async Task BuildAllCharts(bool forceRefresh = false)
        {
            IsBusy = true;
            var tasks = new[]
            {
                BuildGenreChart(forceRefresh),
                BuildWeekdayChart(forceRefresh),
                GetBaseStats(forceRefresh),
                GetMostWatchedShows(forceRefresh),
                BuildTimeOfDayChart(forceRefresh),
                BuildHistoryChart(forceRefresh)
            };
            await Task.WhenAll(tasks);
            IsBusy = false;
        }

        private async Task GetMostWatchedShows(bool forceRefresh = false)
        {
            MostWatchedShows = await _traktStatsService.GetMostWatchedShows(mode: DisplayMode, forceRefresh: forceRefresh);
            var tasks = new List<Task<TraktImage>>();
            foreach (var show in MostWatchedShows)
            {
                if (show.Show?.Ids?.Tmdb != null)
                    tasks.Add(_imageService.GetImageFor(show.Show.Ids.Tmdb.Value));
            }

            var images = await Task.WhenAll(tasks);
            foreach (var image in images)
            {
                var show = MostWatchedShows.FirstOrDefault(m => m.Show.Ids.Tmdb == image.TmdbId);
                if (show != null)
                    show.Show.Image = image;
            }
        }

        private async Task BuildHistoryChart(bool forceRefresh = false)
        {
            var history = await _traktStatsService.GetHistory(HistoryMode, forceRefresh).ConfigureAwait(false);
            var entries = new List<Entry>();
            foreach (var historyItem in history)
            {
                var entry = new Entry(DisplayMode == StatDisplayMode.Plays ? historyItem.NumberOfPlays : historyItem.MinutesPlayed)
                {
                    Color = ColorHelper.GetColorForId(0),
                    ValueLabel = DisplayMode == StatDisplayMode.Plays
                        ? $"{historyItem.NumberOfPlays} Plays"
                        : $"{historyItem.MinutesPlayed} min"
                };
                switch (HistoryMode)
                {
                    case HistoryMode.LastWeek:
                        entry.Label = historyItem.DateTime.ToShortDateString();
                        break;
                    case HistoryMode.LastMonth:
                        entry.Label = $"Week {historyItem.DateTime.WeekNumber()}";
                        break;
                    case HistoryMode.LastYear:
                        entry.Label = historyItem.DateTime.ToString("MMM");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                entries.Add(entry);
            }

            var backgroundColor = (Color)Application.Current.Resources["CardBackgroundColor"];
            HistoryChart = new LineChart
            {
                Entries = entries,
                LabelTextSize = 24f,
                BackgroundColor = backgroundColor.ToSKColor()
            };
        }

        private async Task BuildGenreChart(bool forceRefresh = false)
        {
            var byGenre = await _traktStatsService.GetGenreStats(6, forceRefresh).ConfigureAwait(false);
            if (DisplayMode == StatDisplayMode.Plays)
                byGenre = byGenre.OrderByDescending(b => b.NumberOfPlays).ToList();
            else
                byGenre = byGenre.OrderByDescending(b => b.MinutesPlayed).ToList();

            var entries = new List<Entry>();

            var id = 0;
            foreach (var genre in byGenre)
            {
                entries.Add(new Entry(DisplayMode == StatDisplayMode.Plays ? genre.NumberOfPlays : genre.MinutesPlayed)
                {
                    Label = genre.Genre,
                    ValueLabel = DisplayMode == StatDisplayMode.Plays ? $"{genre.NumberOfPlays} Plays" : TimeHelper.ParseMinutes(genre.MinutesPlayed),
                    Color = ColorHelper.GetColorForId(id),
                });
                id++;
            }
            var backgroundColor = (Color)Application.Current.Resources["CardBackgroundColor"];
            GenreChart = new DonutChart
            {
                Entries = entries,
                LabelTextSize = 20f,
                BackgroundColor = backgroundColor.ToSKColor()
            };
        }

        private async Task GetBaseStats(bool forceRefresh = false)
        {
            BaseStats = await _traktStatsService.GetStats(forceRefresh);
        }

        private async Task BuildWeekdayChart(bool forceRefresh = false)
        {
            var byWeekday = await _traktStatsService.GetWeekdayStats(forceRefresh).ConfigureAwait(false);

            var entries = new List<Entry>();

            var id = 0;
            foreach (var weekday in byWeekday)
            {
                entries.Add(new Entry(DisplayMode == StatDisplayMode.Plays ? weekday.NumberOfPlays : weekday.MinutesPlayed)
                {
                    Label = weekday.Weekday,
                    ValueLabel = DisplayMode == StatDisplayMode.Plays ? $"{weekday.NumberOfPlays} Plays" : TimeHelper.ParseMinutes(weekday.MinutesPlayed),
                    Color = ColorHelper.GetColorForId(id),
                });
                id++;
            }
            var backgroundColor = (Color)Application.Current.Resources["CardBackgroundColor"];
            WeekdayChart = new LineChart()
            {
                Entries = entries,
                LabelTextSize = 24f,
                BackgroundColor = backgroundColor.ToSKColor()
            };
        }

        private async Task BuildTimeOfDayChart(bool forceRefresh = false)
        {
            var byTimeOfDays = await _traktStatsService.GetTimeOfDayStats(forceRefresh).ConfigureAwait(false);

            var entries = new List<Entry>();

            var id = 0;
            foreach (var timeOfDay in byTimeOfDays)
            {
                entries.Add(new Entry(DisplayMode == StatDisplayMode.Plays ? timeOfDay.NumberOfPlays : timeOfDay.MinutesPlayed)
                {
                    Label = timeOfDay.TimeOfDay,
                    ValueLabel = DisplayMode == StatDisplayMode.Plays ? $"{timeOfDay.NumberOfPlays} Plays" : TimeHelper.ParseMinutes(timeOfDay.MinutesPlayed),
                    Color = ColorHelper.GetColorForId(id),
                });
                id++;
            }
            var backgroundColor = (Color)Application.Current.Resources["CardBackgroundColor"];
            TimeOfDayChart = new LineChart()
            {
                Entries = entries,
                LabelTextSize = 16f,
                BackgroundColor = backgroundColor.ToSKColor()
            };
        }
    }
}