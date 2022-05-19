using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape.WeatherForecast.Business.ExceptionRules
{
    public class CustomFluentValidationExceptions : ValidationException
    {
        public CustomFluentValidationExceptions(List<ValidationFailure> validationException) : base(validationException)
        {
        }
    }
}
