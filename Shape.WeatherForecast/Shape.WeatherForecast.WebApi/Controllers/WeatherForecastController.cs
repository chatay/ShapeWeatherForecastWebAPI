using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
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
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : BaseController
    {
        public WeatherForecastController(ILogger<WeatherForecastController> logger, HttpClient httpClient, IOptions<OpenWeatherMapSettings> config, 
            IWeatherForecastService weatherForecastService):base(logger, httpClient, config, weatherForecastService)
        {
        }

        [HttpGet(Name = "GetListOfTemperaturesForCity")]
        public async Task<IActionResult> GetListOfTemperatures([FromQuery] ListOfTempFiveDaysRequest _request)
        {
            return Ok(await _weatherForecastService.GetListOfTemperaturesForCityAsync(_request, new Core.Utilities.RequestOptions { Unit = Core.Utilities.UnitType.Metric, Language = "en", Count = 5}));
        }

        [HttpPost(Name = "GetUserFavoriteLocationsTemp")]
        public async Task<IActionResult> GetUserFavoriteLocationsTemp([FromBody] IEnumerable<int> cityIds)
        {
            return Ok(await _weatherForecastService.GetUserFavoriteLocationsTempAsync(cityIds));
        }
    }
}
