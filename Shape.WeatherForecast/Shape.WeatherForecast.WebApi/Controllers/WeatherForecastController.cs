using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shape.WeatherForecast.Application.DTOs.OpenWeatherMap;
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger, HttpClient httpClient, IOptions<OpenWeatherMapSettings> config)
        {
            _logger = logger;
            _httpClient = httpClient;
            _config = config;
        }

        [HttpGet(Name = "GetListOfTemperatures")]
        public async Task<ListOfTempFiveDaysResponse> Get([FromQuery] string city)
        {
            var weatherForecastapiKey = _config.Value;

            var query = $"https://api.openweathermap.org/data/2.5/weather?q=London,uk&APPID={weatherForecastapiKey.ServiceApiKey}";

            var response = await _httpClient.GetFromJsonAsync<ListOfTempFiveDaysResponse>(query);

            return response;
        }
    }
}
