using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class WishlistItem
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string Genre { get; set; }
        public string Device { get; set; }
    }
}
