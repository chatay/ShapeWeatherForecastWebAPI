using Shape.WeatherForecast.Application.DTOs.OpenWeatherMap;
using Shape.WeatherForecast.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape.WeatherForecast.Application.Interfaces
{
    public interface IWeatherForecastService
    {
        Task<Response<ListOfTempFiveDaysResponse>> GetListOfTemperaturesForCity(string city);
    }
}
