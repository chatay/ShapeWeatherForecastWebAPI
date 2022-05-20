using Shape.WeatherForecast.Application.DTOs.OpenWeatherMap;
using Shape.WeatherForecast.Application.Wrappers;
using Shape.WeatherForecast.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape.WeatherForecast.Application.Interfaces
{
    public interface IWeatherForecastService
    {
        Task<ListOfTempFiveDaysResponse> GetListOfTemperaturesForCityAsync(ListOfTempFiveDaysRequest _request, RequestOptions requestOptions = default);
        Task<ListOfTempFiveDaysResponse> GetUserFavoriteLocationsTempAsync(IEnumerable<int> cityIds, RequestOptions requestOptions = default);
    }
}
