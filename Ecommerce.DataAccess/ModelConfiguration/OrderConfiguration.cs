using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.DataAccess.Model;
 

namespace Ecommerce.DataAccess.ModelConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.ID);

            builder.ToTable("Order");

            builder.Property(e => e.Date).ValueGeneratedOnAdd(); 
            builder.Property(e => e.TotalAmount).HasColumnType("decimal(6, 2)"); 
           
        }
    }
}

