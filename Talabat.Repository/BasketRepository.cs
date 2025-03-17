
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Repositories;


namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer database)
        {
            _database = database.GetDatabase();
        }
        

        public async Task<bool> DeletBasketAsync(string BasketId)
        {
            return await _database.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string BasketId)
        {
            var basket = await _database.StringGetAsync(BasketId);
            return basket.IsNullOrEmpty? null: JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var CreateAndUpdate = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket),TimeSpan.FromDays(30));
            if (CreateAndUpdate == false) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
