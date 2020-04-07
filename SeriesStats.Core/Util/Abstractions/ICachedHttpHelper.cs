using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeriesStats.Core.Util.Abstractions
{
    public interface ICachedHttpHelper
    {
        Task<T> Fetch<T>(HttpClient client, HttpRequestMessage request, TimeSpan cachingTime, bool forceRefresh = false);
        Task<T> Fetch<T>(HttpClient client, string url, TimeSpan cachingTime, bool forceRefresh = false);
    }

    public class CachedHttpHelper : ICachedHttpHelper
    {
        private readonly IConnectivityService _connectivityService;

        public CachedHttpHelper(IConnectivityService connectivityService)
        {
            _connectivityService = connectivityService;
            Barrel.ApplicationId = "SeriesStats";
        }

        public async Task<T> Fetch<T>(HttpClient client, HttpRequestMessage request, TimeSpan cachingTime, bool forceRefresh = false)
        {
            var url = request.RequestUri.ToString();
            var json = string.Empty;

            if (!_connectivityService.HasNetworkConnection())
                json = Barrel.Current.Get<string>(url);
            if (!forceRefresh && !Barrel.Current.IsExpired(url))
                json = Barrel.Current.Get<string>(url);

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        json = await response.Content.ReadAsStringAsync();
                        Barrel.Current.Add(url, json, cachingTime);
                    }
                    else
                    {
                        throw new HttpRequestException($"Error while fetching {url}: {response.ReasonPhrase}");
                    }
                }

                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return default;
            }
        }

        public Task<T> Fetch<T>(HttpClient client, string url, TimeSpan cachingTime, bool forceRefresh = false)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            return Fetch<T>(client, request, cachingTime, forceRefresh);
        }
    }
}
