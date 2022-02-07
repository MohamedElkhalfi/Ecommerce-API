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
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(e => e.ID);

            builder.ToTable("Client"); 

            builder.Property(e => e.Name).HasMaxLength(20).IsUnicode(false);
            builder.Property(e => e.Address).HasMaxLength(100).IsUnicode(false); 
            builder.Property(e => e.Email).HasMaxLength(10).IsUnicode(false);

            builder.Property(e => e.PhoneNumber).HasMaxLength(10).IsUnicode(false);
            builder.Property(e => e.Username).HasMaxLength(15).IsUnicode(false);
        }
    }
}

