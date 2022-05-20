using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shape.WeatherForecast.Application.DTOs.OpenWeatherMap;
using Shape.WeatherForecast.Application.Interfaces;
using Shape.WeatherForecast.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Shape.WeatherForecast.Infrastructure.Shared.Services
{
    public class OpenWeatherMapService : BaseService<OpenWeatherMapService>, IDisposable, IOpenWeatherMapService
    {
        private readonly OpenWeatherMapOptions _options;
        public OpenWeatherMapService(ILogger<OpenWeatherMapService> logger, HttpClient httpClient, IOptions<OpenWeatherMapSettings> config, IDistributedCache distributedCache, OpenWeatherMapOptions options) : base(logger, httpClient, config, distributedCache)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _options.Validate();
            _options = options;
        }
        public async Task<TEntity> RequestAsync<TEntity>(string uri, NameValueCollection queryParameters = null, RequestOptions requestOptions = default, HttpMethod method = null, HttpContent httpContent = null) where TEntity : class
        {
            method ??= HttpMethod.Get;
            requestOptions ??= RequestOptions.Default;
            requestOptions.CancellationToken.ThrowIfCancellationRequested();

            var cacheKey = Caching.UniqueCacheKey(uri, queryParameters);

            var resultRedisCacheData = await _distributedCache.GetStringAsync(cacheKey);
            TEntity result = default(TEntity);
            if (string.IsNullOrEmpty(resultRedisCacheData))
            {
                var requestUri = BuildRequestUri(uri, queryParameters, requestOptions);
                result = await SendAsync<TEntity>(method, requestUri, httpContent);
                resultRedisCacheData = JsonConvert.SerializeObject(result);
                var setTask = _distributedCache.SetStringAsync(cacheKey, resultRedisCacheData, new DistributedCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddHours(1) });
                await Task.WhenAll(setTask);
            }

            if (result == null)
            {
                var forecast = JsonConvert.DeserializeObject<ListOfTempFiveDaysResponse>(resultRedisCacheData);
                return (TEntity)Convert.ChangeType(forecast, typeof(TEntity));
            }

            return result;
        }
        private Uri BuildRequestUri(string uri, NameValueCollection queryParameters = null, RequestOptions requestOptions = default, string version = "2.5")
        {
            requestOptions ??= RequestOptions.Default;

            var parameters = HttpUtility.ParseQueryString(string.Empty);

            if (parameters != null)
            {
                parameters.Add(queryParameters);
            }

            var weatherForecastapiKey = _config.Value;
            parameters.Add("ApiKey", weatherForecastapiKey.ServiceApiKey);
            if (requestOptions.Unit != UnitType.Default)
            {
                parameters.Add("units", requestOptions.Unit == UnitType.Imperial ? "imperial" : "metric");
            }

            if (!string.IsNullOrWhiteSpace(requestOptions.Language))
            {
                parameters.Add("lang", requestOptions.Language);
            }

            if (requestOptions.Count != 0)
            {
                parameters.Add("cnt", requestOptions.Count.ToString());
            }

            var query = parameters.ToString();
            var requestUri = new Uri(_options.BaseAddress, version + "/" + uri);
            var builder = new UriBuilder(requestUri) { Query = query };
            return builder.Uri;
        }
        private async Task<TEntity> SendAsync<TEntity>(HttpMethod httpMethod, Uri uri, HttpContent httpContent = null) where TEntity : class
        {
            using var request = new HttpRequestMessage(httpMethod, uri) { Content = httpContent };
            using var response = await _httpClient.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return default;
            }

            if (!response.IsSuccessStatusCode)
            {
                var responseMessageBuilder = new StringBuilder();
                responseMessageBuilder.Append("OpenWeatherMap Request Error:");
                responseMessageBuilder.AppendFormat("\n Got status {0}: ", response.StatusCode);
                responseMessageBuilder.AppendFormat("\n Request {0} {1}: ", httpMethod.Method, uri);
                responseMessageBuilder.AppendFormat("\n Response Headers: ");

                foreach (var header in response.Headers)
                {
                    responseMessageBuilder.AppendFormat("\n    {0}: \t{1}", header.Key, string.Join(" ,", header.Value));
                }
                responseMessageBuilder.AppendFormat("\n- Response Content: {0}", content);
                throw new InvalidOperationException(responseMessageBuilder.ToString());
            }
            return JsonConvert.DeserializeObject<TEntity>(content);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
