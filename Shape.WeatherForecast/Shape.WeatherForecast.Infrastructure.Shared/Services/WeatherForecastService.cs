using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shape.WeatherForecast.Application.DTOs.OpenWeatherMap;
using Shape.WeatherForecast.Application.Interfaces;
using Shape.WeatherForecast.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Shape.WeatherForecast.Infrastructure.Shared.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly ILogger<WeatherForecastService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IOptions<OpenWeatherMapSettings> _config;

        public WeatherForecastService(ILogger<WeatherForecastService> logger, HttpClient httpClient, IOptions<OpenWeatherMapSettings> config)
        {
            _logger = logger;
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<Response<ListOfTempFiveDaysResponse>> GetListOfTemperaturesForCity(string city)
        {
            var weatherForecastapiKey = _config.Value;

            var query = $"https://api.openweathermap.org/data/2.5/weather?q=London,uk&APPID={weatherForecastapiKey.ServiceApiKey}";

            var response = await _httpClient.GetFromJsonAsync<ListOfTempFiveDaysResponse>(query);

            return new Response<ListOfTempFiveDaysResponse>(response);
        }
    }
}
