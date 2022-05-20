using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape.WeatherForecast.Core.Utilities
{
    public static class Caching
    {
        public static string UniqueCacheKey(string uri, NameValueCollection parameters)
        {
            if (parameters is null)
            {
                return $"req-{uri}";
            }

            var combinedParameters = parameters.Keys
                .Cast<string>()
                .Select(s => s + ":" + parameters[s]);

            return $"req-{uri}-{{{string.Join(";", combinedParameters)}}}";
        }
    }
}
