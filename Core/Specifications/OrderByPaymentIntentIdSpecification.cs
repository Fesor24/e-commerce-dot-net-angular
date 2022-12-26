using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.Specifications
{
    public class OrderByPaymentIntentIdSpecification: BaseSpecification<Order>
    {
        public OrderByPaymentIntentIdSpecification(string paymentIntentId): base(x => x.PaymentIntentId == paymentIntentId)
        {

        }
    }
}
