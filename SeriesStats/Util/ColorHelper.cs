using SkiaSharp;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace SeriesStats.Util
{
    public static class ColorHelper
    {
        private static readonly List<SKColor> _lightChartColors = new List<SKColor>
        {
            SKColor.Parse("#003f5c"),
            SKColor.Parse("#2f4b7c"),
            SKColor.Parse("#665191"),
            SKColor.Parse("#a05195"),
            SKColor.Parse("#d45087"),
            SKColor.Parse("#f95d6a"),
            SKColor.Parse("#ff7c43"),
            SKColor.Parse("#ffa600")
        };

        public static SKColor GetColorForId(int id)
        {
            var realId = id % _lightChartColors.Count;
            return _lightChartColors[realId];
        }
    }
}