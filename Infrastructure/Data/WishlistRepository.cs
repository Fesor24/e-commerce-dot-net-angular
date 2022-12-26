using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly IDatabase database;

        public WishlistRepository(IConnectionMultiplexer redis)
        {
            this.database  = redis.GetDatabase();
        }
        public async Task<bool> DeleteWishlist(string wishlistId)
        {
            return await database.KeyDeleteAsync(wishlistId);
        }

        public async Task<Wishlist> GetWishlistAsync(string wishlistId)
        {
            var data = await database.StringGetAsync(wishlistId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Wishlist>(data);
        }

        public async Task<Wishlist> UpdateWishlist(Wishlist wishlist)
        {
            var created = await database.StringSetAsync(wishlist.Id, JsonSerializer.Serialize(wishlist), TimeSpan.FromDays(30));

            if (!created) return null;

            return await GetWishlistAsync(wishlist.Id);
        }
    }
}
