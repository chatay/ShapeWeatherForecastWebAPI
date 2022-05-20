using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape.WeatherForecast.Core.Utilities
{
    public class OpenWeatherMapOptions
    {
        public string ApiKey { get; set; }
        public Uri BaseAddress { get; set; } = new Uri("https://api.openweathermap.org/data/");

        public void Validate()
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new InvalidOperationException("No api key defined");
            }
            if (BaseAddress == null)
            {
                throw new InvalidOperationException("No base address defined.");
            }
        }
    }
}
