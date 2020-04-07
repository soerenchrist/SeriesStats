namespace SeriesStats.Core.Util.Abstractions
{
    public interface IApiConfiguration
    {
        string TraktClientId { get; }
        string TraktClientSecret { get; }
        string TmdbApiKey { get; }
    }
}