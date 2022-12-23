using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(x => x.ShipToAddress, b =>
            {
                b.WithOwner();
            });

            builder.Property(s => s.Status)
                .HasConversion(p => p.ToString(),
                p => (OrderStatus) Enum.Parse(typeof(OrderStatus), p));

            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.SubTotal).HasColumnType("decimal(18,2)");
        }
    }
}
