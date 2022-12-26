using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IWishlistRepository
    {
        Task<Wishlist> GetWishlistAsync(string wishlistId);

        Task<Wishlist> UpdateWishlist(Wishlist wishlist);

        Task<bool> DeleteWishlist(string wishlistId);
    }
}
