using SeriesStats.Core.Util;
using SeriesStats.Core.Util.Abstractions;
using Xamarin.Essentials;

namespace SeriesStats.Util
{
    public class ConnectivityService : IConnectivityService
    {
        public bool HasNetworkConnection()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }
    }
}