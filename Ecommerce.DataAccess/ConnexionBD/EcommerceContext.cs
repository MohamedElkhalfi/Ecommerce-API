using Microsoft.EntityFrameworkCore;
using Ecommerce.DataAccess.Model;
using Ecommerce.DataAccess.ModelConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.ConnexionDB
{
   public class EcommerceContext : DbContext
    {
        private bool IsTestContext { get; }
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options)
        {
        }

        public EcommerceContext(DbContextOptions<EcommerceContext> options, bool isTestContext) : base(options)
        {
            IsTestContext = isTestContext;
        }

        public EcommerceContext()
        {
        }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {
                modelBuilder.HasAnnotation("EcommerceVersion", "1.1");

                if (!IsTestContext)
                {
                    modelBuilder.HasDefaultSchema("Ecommerce");
                }
                modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
                modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
                modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientConfiguration).Assembly);
                modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderItemConfiguration).Assembly);
                modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
            }
        }

        const string connectionString = "Data Source=.;Initial Catalog=Ecommerce;Integrated Security=True";

 

     

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }


    }
}
