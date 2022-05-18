using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shape.WeatherForecast.Application.DTOs.OpenWeatherMap;
using Shape.WeatherForecast.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shape.WeatherForecast.WebApi.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogger<WeatherForecastController> _logger;
        protected readonly HttpClient _httpClient;
        protected readonly IOptions<OpenWeatherMapSettings> _config;
        protected readonly IWeatherForecastService _weatherForecastService;

        public BaseController(ILogger<WeatherForecastController> logger, HttpClient httpClient, IOptions<OpenWeatherMapSettings> config, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _httpClient = httpClient;
            _config = config;
            _weatherForecastService = weatherForecastService;
        }
    }
}
