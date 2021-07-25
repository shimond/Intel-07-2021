using AspIntro.WebApi.ActionFilters;
using AspIntro.WebApi.Contracts;
using AspIntro.WebApi.Models.Config;
using AspIntro.WebApi.Models.Dtos;
using AspIntro.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

namespace AspIntro.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RedisConfiguration>(Configuration.GetSection("Redis"));
            services.AddSingleton<MySingleService>();
            services.AddAutoMapper(typeof(Startup));
            //services.AddDistributedMemoryCache();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetSection("Redis")["ConnectionString"];
            });
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IProductsRepository, MemoryProductRepository>();
            services.AddScoped<CourseApiExceptionActionFilter>();
            services.AddScoped<ValidationActionFilter>();
            services.AddScoped<AddCreatedByActionFilter<ProductDto>>();
            
            services.AddControllers(p =>
            {
                //p.Filters.AddService(typeof(CourseApiExceptionActionFilter);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AspIntro.WebApi", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await next();
            });

            app.UseResponseCaching(); // Cities, Countries, Streets

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspIntro.WebApi v1"));
            }

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
