using Microsoft.Extensions.DependencyInjection;
using Shape.WeatherForecast.Application.Interfaces;
using Shape.WeatherForecast.Core.Utilities;
using Shape.WeatherForecast.Infrastructure.Shared.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape.WeatherForecast.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static OpenWeatherMapOptions UserOptions { get; set; } = new OpenWeatherMapOptions();

        public static void AddSharedInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            services.AddScoped<IOpenWeatherMapService, OpenWeatherMapService>();
        }
    }
}