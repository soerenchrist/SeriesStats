using SeriesStats.Core.Util;
using SeriesStats.Core.Util.Abstractions;
using Xamarin.Essentials;

namespace SeriesStats.Util
{
    public class LocationProvider : ILocationProvider
    {
        public string GetAppLocation()
        {
            return FileSystem.AppDataDirectory;
        }
    }
}