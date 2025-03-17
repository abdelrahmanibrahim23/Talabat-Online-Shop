using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;

namespace Talabat.Repository.Data.Config
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        void IEntityTypeConfiguration<Product>.Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p=>p.PictureUrl).IsRequired();
            builder.HasOne(p => p.ProductBreand).WithMany().HasForeignKey(p => p.ProductBreandId);
            builder.HasOne(p=>p.ProductTypes).WithMany().HasForeignKey(p=>p.ProductTypeId);
            builder.Property(P=>P.Price).HasColumnType("decimal(18,2)");
        }
    }
}
