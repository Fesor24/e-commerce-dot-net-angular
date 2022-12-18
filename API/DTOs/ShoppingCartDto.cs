using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace API.DTOs
{
    public class ShoppingCartDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public List<ShoppingCartItemDto> Items { get; set; } = new List<ShoppingCartItemDto>();
    }
}
