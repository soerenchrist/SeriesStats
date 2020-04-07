using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SeriesStats.Core.Models.MovieDb.Image;
using SeriesStats.Core.Models.Trakt.Base;
using SeriesStats.Core.Services.MovieDb.Abstractions;
using SeriesStats.Core.Util;
using SeriesStats.Core.Util.Abstractions;

namespace SeriesStats.Core.Services.MovieDb
{
    public class ImageService : IImageService
    {
        private readonly ICachedHttpHelper _cachedHttpHelper;
        private readonly IApiConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ImageService(ICachedHttpHelper cachedHttpHelper,
            IApiConfiguration configuration)
        {
            _cachedHttpHelper = cachedHttpHelper;
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        public async Task<TraktImage> GetImageFor(int showId)
        {
            var url =
                $"https://api.themoviedb.org/3/tv/{showId}/images?api_key={_configuration.TmdbApiKey}&language=en";

            var showImages = await _cachedHttpHelper.Fetch<ShowImages>(_httpClient, url, TimeSpan.FromDays(5));
            if (showImages == null) return null;
            if (showImages.Posters.Count > 0)
            {
                return new TraktImage
                {
                    ImagePath = showImages.Posters.First().FilePath,
                    TmdbId = showId
                };
            }

            var backdropPath = showImages.Backdrops.FirstOrDefault()?.FilePath ?? "";

            if (string.IsNullOrWhiteSpace(backdropPath))
            {
                return null;
            }

            return new TraktImage
            {
                TmdbId = showId,
                ImagePath = backdropPath
            };
        }
    }
}