using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using SeriesStats.Core.Models.Auth;
using SeriesStats.Core.Repository.Abstractions;
using SeriesStats.Core.Util.Abstractions;

namespace SeriesStats.Core.Repository
{
    public class LiteDbTokenRepository : ITokenRepository
    {
        private readonly LiteDatabase _database;

        public LiteDbTokenRepository(ILocationProvider locationProvider)
        {
            var path = Path.Combine(locationProvider.GetAppLocation(), "Auth.db");
            _database = new LiteDatabase(path);
        }

        public Task SaveAccessToken(AccessTokenResponse accessToken)
        {
            var collection = _database.GetCollection<AccessTokenResponse>();
            collection.DeleteAll();
            collection.Insert(accessToken);
            return Task.CompletedTask;
        }

        public Task<AccessTokenResponse> GetAccessToken()
        {
            var collection = _database.GetCollection<AccessTokenResponse>();
            var token = collection.FindAll().FirstOrDefault();
            return Task.FromResult(token);
        }

        public void RemoveAccessToken()
        {
            var collection = _database.GetCollection<AccessTokenResponse>();
            collection.DeleteAll();
        }
    }
}