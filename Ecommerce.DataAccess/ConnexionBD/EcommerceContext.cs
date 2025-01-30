using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ecommerce.DataAccess.Model;
using Ecommerce.DataAccess.ModelConfiguration;
using System;
using System.IO;

namespace Ecommerce.DataAccess.ConnexionDB
{
    public class EcommerceContext : DbContext
    {
        private bool IsTestContext { get; }
        private readonly string _connectionString;

        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options)
        {
        }

        public EcommerceContext(DbContextOptions<EcommerceContext> options, bool isTestContext) : base(options)
        {
            IsTestContext = isTestContext;
        }

        public EcommerceContext()
        {
           
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Définit le répertoire de base
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Charge le fichier principal
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true) // Charge la version spécifique de l'environnement
                .Build();

            _connectionString = configuration.GetConnectionString("EcoContext"); // Récupère la chaîne de connexion
         
          
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) // Évite d'écraser la config si elle a déjà été définie
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
    }
}
