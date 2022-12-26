using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Wishlist
    {
        public Wishlist()
        {

        }

        public Wishlist(string id)
        {
            Id = id;
        }
        public string Id { get; set; }

        public List<WishlistItem> Items { get; set; } = new List<WishlistItem>();
    }
}
