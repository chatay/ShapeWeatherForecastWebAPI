using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shape.WeatherForecast.Application.DTOs.OpenWeatherMap;
using Shape.WeatherForecast.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shape.WeatherForecast.Infrastructure.Shared.Services
{
    public class BaseService<TEntity>
    {
        protected readonly ILogger<TEntity> _logger;
        protected readonly HttpClient _httpClient;
        protected readonly IOptions<OpenWeatherMapSettings> _config;
        protected IDistributedCache _distributedCache;
        protected IOpenWeatherMapService _openWeatherMapService;
        public BaseService(ILogger<TEntity> logger, HttpClient httpClient, IOptions<OpenWeatherMapSettings> config, IDistributedCache distributedCache = null, IOpenWeatherMapService openWeatherMapService = null)
        {
            _logger = logger;
            _httpClient = httpClient;
            _config = config;
            _distributedCache = distributedCache;
            _openWeatherMapService = openWeatherMapService;
        }
    }
}
