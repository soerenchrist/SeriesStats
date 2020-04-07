using SeriesStats.Core.Util.Abstractions;
using SeriesStats.Helpers;
using SeriesStats.Util.Abstractions;

namespace SeriesStats.Util
{
    public class ApiConfiguration : IApiConfiguration
    {
        public string TraktClientId => Secrets.TraktClientId;
        public string TraktClientSecret => Secrets.TraktClientSecret;
        public string TmdbApiKey => Secrets.TmdbApiKey;
    }
}