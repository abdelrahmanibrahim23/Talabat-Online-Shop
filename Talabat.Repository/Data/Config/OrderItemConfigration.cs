using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity.OrderAgregation;

namespace Talabat.Repository.Data.Config
{
    public class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(O => O.Product, NP => NP.WithOwner());
            builder.Property(O=>O.Price).HasColumnType("decimal(18,2)");
        }
    }
}
