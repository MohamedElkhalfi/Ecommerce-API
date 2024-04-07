using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DACnxDb = Ecommerce.DataAccess.ConnexionDB;
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
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Ajoutez le support CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOriginPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin() // Autoriser toutes les origines
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce.Api v1"));
            }

            app.UseHttpsRedirection();
            // Utilisez le middleware CORS
            // Middleware CORS - Placez-le entre UseRouting() et UseEndpoints()
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

          

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public virtual void SetUpMvcConfiguration(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        public virtual void SetUpDataBase(IServiceCollection services)
        {
            services.AddDbContext<DACnxDb.EcommerceContext>(options => 
                                    options.UseSqlServer(Configuration.GetConnectionString("EcoContext"))); 
        }
    }
}
