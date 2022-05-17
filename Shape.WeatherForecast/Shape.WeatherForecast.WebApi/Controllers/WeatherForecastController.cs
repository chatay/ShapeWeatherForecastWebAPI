using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shape.WeatherForecast.Application.DTOs.OpenWeatherMap;
using Shape.WeatherForecast.Application.Interfaces;
using Shape.WeatherForecast.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Shape.WeatherForecast.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IOptions<OpenWeatherMapSettings> _config;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, HttpClient httpClient, IOptions<OpenWeatherMapSettings> config, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _httpClient = httpClient;
            _config = config;
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet(Name = "GetListOfTemperaturesForCity")]
        public async Task<IActionResult> GetListOfTemperatures([FromQuery] string city)
        {
            return Ok(await _weatherForecastService.GetListOfTemperaturesForCity(city));
        }
    }
}
