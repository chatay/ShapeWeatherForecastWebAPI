using FluentValidation;
using Shape.WeatherForecast.Application.DTOs.OpenWeatherMap;
using Shape.WeatherForecast.Business.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape.WeatherForecast.Business.Validation.FluentValidation
{
    public class WeatherForecastValidator : AbstractValidator<ListOfTempFiveDaysRequest>
    {
        public WeatherForecastValidator()
        {
            RuleFor(x => x.City).WeatherForeCastInput();
        }
    }
}
