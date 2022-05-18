using Microsoft.Extensions.Caching.Distributed;
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
using System.Text.Json;
using System.Threading.Tasks;

namespace Shape.WeatherForecast.Infrastructure.Shared.Services
{
    public class WeatherForecastService : BaseService, IWeatherForecastService
    {

        public WeatherForecastService(ILogger<WeatherForecastService> logger, HttpClient httpClient, IOptions<OpenWeatherMapSettings> config, IDistributedCache _distributedCache) : base(logger, httpClient, config, _distributedCache)
        {
        }
        public async Task<Response<ListOfTempFiveDaysResponse>> GetListOfTemperaturesForCity(string city)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            var weatherForecastapiKey = _config.Value;
            string redisJson;
            var query = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&APPID={weatherForecastapiKey.ServiceApiKey}&cnt=5";

            //var response = await _httpClient.GetFromJsonAsync<ListOfTempFiveDaysResponse>(query);

            var keyName = $"forecast:{city}";
            redisJson = await _distributedCache.GetStringAsync(keyName);
            if (string.IsNullOrEmpty(redisJson))
            {
                var response = await _httpClient.GetFromJsonAsync<ListOfTempFiveDaysResponse>(query);
                redisJson = JsonSerializer.Serialize(response);
                var setTask = _distributedCache.SetStringAsync(keyName, redisJson, new DistributedCacheEntryOptions{ AbsoluteExpiration = DateTime.Now.AddHours(1) });
                await Task.WhenAll(setTask);
            }

            var forecast =
                    JsonSerializer.Deserialize<ListOfTempFiveDaysResponse>(redisJson);

            return new Response<ListOfTempFiveDaysResponse>(forecast);
        }
    }
}
