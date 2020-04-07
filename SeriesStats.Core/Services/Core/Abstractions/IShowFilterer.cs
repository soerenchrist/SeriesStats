using SeriesStats.Core.Models.Core;
using SeriesStats.Core.Models.Trakt;

namespace SeriesStats.Core.Services.Core.Abstractions
{
    public interface IShowFilterer
    {
        bool FilterApplies(TraktShow show, FilterOptions options);
    }
}