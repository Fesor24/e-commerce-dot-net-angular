using Core.Entities.OrderAggregate;

namespace API.DTOs
{
    public class OrderToReturnDto
    {
        /// <summary>
        /// The id 
        /// </summary>
        public int Id { get; set; }
        public DateTimeOffset OrderDate { get; set; }

        /// <summary>
        /// Address product will be shipped to
        /// </summary>
        public Address ShipToAddress { get; set; }

        /// <summary>
        /// Delivery method chosen by buyer
        /// </summary>
        public string DeliveryMethods { get; set; }

        /// <summary>
        /// Shipping price of the order
        /// </summary>
        public decimal ShippingPrice { get; set; }

        /// <summary>
        /// The list of order items of a buyer
        /// </summary>
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }

        /// <summary>
        /// Total of order items and quantity added together
        /// </summary>
        public decimal SubTotal { get; set; }

        /// <summary>
        /// Total price
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Status of order
        /// </summary>
        public string Status { get; set; }

    }
}
