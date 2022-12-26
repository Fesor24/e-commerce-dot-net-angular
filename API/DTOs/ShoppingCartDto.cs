using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace API.DTOs
{
    public class ShoppingCartDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public List<ShoppingCartItemDto> Items { get; set; }

        public int? DeliveryMethodId { get; set; }

        public string? ClientSecret { get; set; }

        public string? PaymentIntentId { get; set; }

        public decimal ShippingPrice { get; set; }
    }
}
