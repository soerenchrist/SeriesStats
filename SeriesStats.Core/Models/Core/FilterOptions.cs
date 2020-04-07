using System;

namespace SeriesStats.Core.Models.Core
{
    public class FilterOptions
    {
        public bool Continued { get; set; }
        public bool NotViewed { get; set; }
        public bool Favorite { get; set; }

        public static FilterOptions FromString(string filterKey)
        {
            if (string.IsNullOrWhiteSpace(filterKey))
                return new FilterOptions();
            var parts = filterKey.Split(';');
            if (parts.Length == 0) return new FilterOptions();

            var option = new FilterOptions();
            foreach (var part in parts)
            {
                if (part.Equals(nameof(Continued), StringComparison.OrdinalIgnoreCase))
                {
                    option.Continued = true;
                }

                if (part.Equals(nameof(NotViewed), StringComparison.OrdinalIgnoreCase))
                {
                    option.NotViewed = true;
                }

                if (part.Equals(nameof(Favorite), StringComparison.OrdinalIgnoreCase))
                {
                    option.Favorite = true;
                }
            }

            return option;
        }

        public override string ToString()
        {
            var value = "";
            if (Continued)
            {
                value += nameof(Continued) + ";";
            }

            if (NotViewed)
            {
                value += nameof(NotViewed) + ";";
            }
            if (Favorite)
            {
                value += nameof(Favorite) + ";";
            }

            return value;
        }
    }
}