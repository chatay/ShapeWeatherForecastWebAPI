using FluentValidation;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shape.WeatherForecast.Application.DTOs.OpenWeatherMap;
using Shape.WeatherForecast.Application.Interfaces;
using Shape.WeatherForecast.Application.Wrappers;
using Shape.WeatherForecast.Business.ExceptionRules;
using Shape.WeatherForecast.Business.Validation.FluentValidation;
using Shape.WeatherForecast.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shape.WeatherForecast.Infrastructure.Shared.Services
{
    public class WeatherForecastService : BaseService<WeatherForecastService>, IWeatherForecastService
    {
        private readonly OpenWeatherMapOptions _options;
        public WeatherForecastService(ILogger<WeatherForecastService> logger, HttpClient httpClient, IOptions<OpenWeatherMapSettings> config, IDistributedCache _distributedCache, IOpenWeatherMapService _openWeatherMapService, OpenWeatherMapOptions options) : base(logger, httpClient, config, _distributedCache, _openWeatherMapService)
        {
            _options = options;
        }
        public async Task<ListOfTempFiveDaysResponse> GetListOfTemperaturesForCity(ListOfTempFiveDaysRequest _request, RequestOptions requestOptions = default)
        {
            WeatherForecastValidator fiveDaysTempratureValidator = new WeatherForecastValidator();
            var result = fiveDaysTempratureValidator.Validate(_request);
            if (!result.IsValid)
            {
                throw new CustomFluentValidationExceptions(result.Errors);
            }

            requestOptions ??= RequestOptions.Default;
            requestOptions.CancellationToken.ThrowIfCancellationRequested();


            var parameters = new NameValueCollection { { "q", _request.City } };
            return await _openWeatherMapService.RequestAsync<ListOfTempFiveDaysResponse>("forecast", parameters, requestOptions); ;
        }

        //public Task<Response<ListOfTempFiveDaysResponse>> GetUserFavoriteLocationsTemp(ListOfTempFiveDaysRequest _request)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
