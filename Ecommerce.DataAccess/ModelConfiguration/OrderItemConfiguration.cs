using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.DataAccess.Model;
 

namespace Ecommerce.DataAccess.ModelConfiguration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(e => e.ID);

            builder.ToTable("OrderItem");
             
            builder.Property(e => e.Price).HasColumnType("decimal(6, 2)"); 
            builder.Property(e => e.Quantity).HasColumnType("decimal(6, 2)"); 


        }
    }
}

