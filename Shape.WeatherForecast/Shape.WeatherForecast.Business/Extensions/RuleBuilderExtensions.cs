using FluentValidation;
using Shape.WeatherForecast.Business.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape.WeatherForecast.Business.Extensions  
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilder<T, string> WeatherForeCastInput<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .NotEmpty().WithMessage(Messages.CityRequestInput)
                .Must(x => x.All(char.IsLetter)).WithMessage(Messages.StringContainsNumber)
                .MinimumLength(3).WithMessage("length is too short");

            return options;
        }
    }
}
