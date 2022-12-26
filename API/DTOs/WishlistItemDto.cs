using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class WishlistItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string GameName { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
      
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Device { get; set; }
    }
}
