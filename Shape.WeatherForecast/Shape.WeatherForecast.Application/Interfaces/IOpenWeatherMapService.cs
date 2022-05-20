using Shape.WeatherForecast.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shape.WeatherForecast.Application.Interfaces
{
    public interface IOpenWeatherMapService
    {
        Task<TEntity> RequestAsync<TEntity>(string url, NameValueCollection queryParameters = null, RequestOptions requestOptions = default, HttpMethod method = null,  HttpContent httpContent = null) where TEntity : class;
    }
}
