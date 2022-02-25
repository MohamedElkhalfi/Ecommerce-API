using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.ModelConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.ID).ValueGeneratedOnAdd();
            
            builder.ToTable("Product");

            builder.Property(e => e.Is_Available).HasDefaultValue(true);
            builder.Property(e => e.Is_Selected).HasDefaultValue(true);
            builder.Property(e => e.Is_Promotion).HasDefaultValue(true);

            builder.Property(e => e.Name).HasMaxLength(20).IsUnicode(false);
            builder.Property(e => e.Description).HasMaxLength(100).IsUnicode(false).IsRequired(false); 

            builder.Property(e => e.CurrentPrice).HasColumnType("decimal(6, 2)");
            builder.Property(e => e.Quantity).HasColumnType("decimal(6, 2)");

            builder.HasOne(d => d.Category_)
                .WithMany(p => p.Product_)
                .HasForeignKey(d => d.ID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Product_Category");
        }
    }
}

