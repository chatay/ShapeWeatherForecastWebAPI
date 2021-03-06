using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Shape.WeatherForecast.Application.DTOs.OpenWeatherMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shape.WeatherForecast.Infrastructure.Shared;
using Shape.WeatherForecast.Core.Utilities;

namespace Shape.WeatherForecast.WebApi
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
            services.Configure<OpenWeatherMapSettings>(Configuration.GetSection("OpenWeatherMapSettings"));
            services.AddHttpClient();
            services.AddControllers();
            services.AddSharedInfrastructure();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shape.WeatherForecast.WebApi", Version = "v1" });
            });
            services.AddStackExchangeRedisCache(options => {
                options.Configuration = "localhost:6380";

            });
            services.AddSingleton(new OpenWeatherMapOptions
            {
                ApiKey = Configuration["OpenWeatherMapSettings:ServiceApiKey"]
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shape.WeatherForecast.WebApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
