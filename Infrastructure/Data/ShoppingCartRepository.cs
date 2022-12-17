using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IDatabase database;

        public ShoppingCartRepository(IConnectionMultiplexer redis)
        {
            this.database = redis.GetDatabase();
        }
        public async Task<bool> DeleteShoppingCartAsync(string cartId)
        {
            return await database.KeyDeleteAsync(cartId);
        }

        public async Task<ShoppingCart> GetShoppingCartAsync(string cartId)
        {
            var data = await database.StringGetAsync(cartId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<ShoppingCart>(data);
        }

        public async Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart cart)
        {
            var created = await database.StringSetAsync(cart.Id, JsonSerializer.Serialize(cart), TimeSpan.FromDays(30));

            //if the basket was not created, we return null
            if (!created) return null;

            return await GetShoppingCartAsync(cart.Id);
        }
    }
}
