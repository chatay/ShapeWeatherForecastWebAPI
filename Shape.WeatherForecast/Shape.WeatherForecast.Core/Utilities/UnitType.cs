using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape.WeatherForecast.Core.Utilities
{
    public enum UnitType
    {
        /// <summary>
        /// temprature is returned in Kelvin
        /// </summary>
        Default,
        /// <summary>
        /// temprature is returned in Celsius
        /// </summary>
        Metric,
        /// <summary>
        /// temprature is returned in Fahrenheit
        /// </summary>
        Imperial
    }
}
