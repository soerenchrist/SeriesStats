using System;

namespace SeriesStats.Util
{
    public static class TimeHelper
    {
        public static string ParseMinutes(int minutes)
        {
            var timespan = TimeSpan.FromMinutes(minutes);
            var days = timespan.Days;
            var hours = timespan.Hours;
            var min = timespan.Minutes;

            if (days == 0)
            {
                return $"{hours} hours, {minutes} minutes";
            }

            return $"{days} days, {hours} hours";
        }
    }
}