using Microsoft.EntityFrameworkCore;
using Ecommerce.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.DataAccess.ModelConfiguration
{
    class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => e.ID);
            builder.ToTable("Category"); 
            builder.Property(e => e.Name).HasMaxLength(50).IsUnicode(false);  
            builder.Property(e => e.Description) .HasMaxLength(100).IsUnicode(false);   

        }
    }
}
