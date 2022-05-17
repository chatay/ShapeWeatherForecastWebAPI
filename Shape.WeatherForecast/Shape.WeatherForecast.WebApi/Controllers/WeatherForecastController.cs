using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shape.WeatherForecast.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly HttpClient _httpClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        [HttpGet(Name = "GetListOfTemperatures")]
        public async Task<string> Get([FromQuery] string cities)
        {
            string apiKey = "aec690a9abbd82ae0ec25b560f060c73";
            var query = $"https://api.openweathermap.org/data/2.5/weather?q=London,uk&APPID={apiKey}";

            var response = await _httpClient.GetAsync(query);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "something went wrong.";
            }
        }
    }
}
