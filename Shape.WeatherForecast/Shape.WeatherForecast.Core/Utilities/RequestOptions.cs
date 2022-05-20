using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shape.WeatherForecast.Core.Utilities
{
    //prevents other classes from inheriting from it
    public sealed class RequestOptions
    {
        public static RequestOptions Default { get; set; } = new RequestOptions();
        public CancellationToken CancellationToken { get; set; }

        public string Language { get; set; }

        public UnitType Unit{ get; set; }
        public int Count{ get; set; }
    }
}
