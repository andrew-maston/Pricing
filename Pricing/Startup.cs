using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pricing.Repositories;
using Pricing.Services;
using System;
using System.IO;

namespace Pricing
{
    public class Startup
    {
        private string _tableStorageAccountConnectionString { get; }

        public Startup(IConfiguration configuration)
        {
            _tableStorageAccountConnectionString = configuration.GetConnectionString("TableStorageConnectionString");
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(service => CloudStorageAccount.Parse(_tableStorageAccountConnectionString));

            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IOfferRepository, OfferRepository>();

            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IOfferService, OfferService>();
            services.AddSingleton<IInvoiceService, InvoiceService>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "Pricing.xml");
                c.IncludeXmlComments(filePath);
            });
        }
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
