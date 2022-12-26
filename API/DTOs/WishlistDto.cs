using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class WishlistDto
    {
        [Required]
        public string Id { get; set; }
        public List<WishlistItemDto> Items { get; set; }
    }
}
