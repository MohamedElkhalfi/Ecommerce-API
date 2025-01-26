using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Core.Interfaces;
using Ecommerce.DataAccess.Repositories;
using Ecommerce.Core.Services;
using Ecommerce.Api.Dto.Interfaces.BusinessToApi;
using Ecommerce.Api.Dto.Mapping;
using Ecommerce.DataAccess.Dto.Mapping;
using Ecommerce.DataAccess.Dto.Interfaces;

namespace Ecommerce.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // Configuration de log4net
            ConfigureLog4Net();
        }

        public IConfiguration Configuration { get; }

        private void ConfigureLog4Net()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuration de CORS en utilisant un tableau d'origines
            var allowedOrigins = Configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder.WithOrigins(allowedOrigins) // Utilisation d'un tableau d'origines
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            SetUpMvcConfiguration(services);
            SetUpDataBase(services);

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductInterface, ProductApiMap>();
            services.AddScoped<IProductMapping, ProductMapping>();

            // Configuration de Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce.Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowSpecificOrigin"); // Appliquer la politique CORS configurée

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public virtual void SetUpMvcConfiguration(IServiceCollection services)
        {
            services.AddControllers();
        }

        public virtual void SetUpDataBase(IServiceCollection services)
        {
            services.AddDbContext<Ecommerce.DataAccess.ConnexionDB.EcommerceContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("EcoContext")));
        }
    }
}
