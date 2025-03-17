using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class CachResponse : IResponceCaching
    {
        private readonly IDatabase _database;
        public CachResponse(IConnectionMultiplexer database)
        {
            _database=database.GetDatabase();
        }
        public async Task CachingResponseAsync(string cachKey, object responce, TimeSpan timeToLive)
        {
            if (responce == null) return;
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var responseSerialize= JsonSerializer.Serialize(responce, options);
             await _database.StringSetAsync(cachKey, responseSerialize, timeToLive);
        }

        public async Task<string> GetCachingResponseAsync(string cachKey)
        {
            var response = await _database.StringGetAsync(cachKey);
            if(response.IsNullOrEmpty)return null;
            return response;
        }
    }
}
