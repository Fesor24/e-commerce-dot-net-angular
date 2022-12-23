using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrdersWithDeliveryAndOrderingSpecification : BaseSpecification<Order>
    {
        public OrdersWithDeliveryAndOrderingSpecification(string email) : base(x => x.BuyerEmail == email)
        {
            AddIncludes(x => x.OrderItems);
            AddIncludes(x => x.DeliveryMethods);
            AddOrderByDescending(x => x.OrderDate);
        }

        public OrdersWithDeliveryAndOrderingSpecification(int orderId, string email): base(x => x.Id == orderId && x.BuyerEmail == email)
        {
            AddIncludes(x => x.OrderItems);
            AddIncludes(x => x.DeliveryMethods);
        }
    }
}
