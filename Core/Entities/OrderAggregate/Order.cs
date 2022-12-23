using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {

        }
        public Order(IReadOnlyList<OrderItem> orderItems, string buyerEmail, Address shipToAddress, decimal subTotal, DeliveryMethod deliveryMethod)
        {
            OrderItems = orderItems;
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            SubTotal = subTotal;
            DeliveryMethods = deliveryMethod;
        }

        /// <summary>
        /// We will use this to get the orders of a buyer
        /// </summary>
        public string BuyerEmail { get; set; }

        /// <summary>
        /// Time order was made
        /// </summary>
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Address product will be shipped to
        /// </summary>
        public Address ShipToAddress { get; set; }

        /// <summary>
        /// Delivery method chosen by buyer
        /// </summary>
        public DeliveryMethod DeliveryMethods { get; set; }

        /// <summary>
        /// The list of order items of a buyer
        /// </summary>
        public IReadOnlyList<OrderItem> OrderItems { get; set; }

        /// <summary>
        /// Total of order items and quantity added together
        /// </summary>
        public decimal SubTotal { get; set; }

        /// <summary>
        /// Status of order
        /// </summary>
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        /// <summary>
        /// Payment method of buyer
        /// </summary>
        public string PaymentIntentId { get; set; }

        /// <summary>
        /// Sums up the subtotal and delivery method price
        /// </summary>
        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethods.Price;
        }
    }
}
