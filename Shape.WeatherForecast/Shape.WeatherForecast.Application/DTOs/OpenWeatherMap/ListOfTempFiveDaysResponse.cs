using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape.WeatherForecast.Application.DTOs.OpenWeatherMap
{
    public class ListOfTempFiveDaysResponse
    {
        [JsonProperty("cod")]
        public long Cod { get; set; }

        [JsonProperty("message")]
        public long Message { get; set; }

        [JsonProperty("cnt")]
        public long Cnt { get; set; }

        [JsonProperty("list")]
        public IList<ListInfoAboutWeather> List { get; set; }

        [JsonProperty("city")]
        public City City { get; set; }
    }
    public partial class City
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("coord")]
        public Coord Coord { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("population")]
        public long Population { get; set; }

        [JsonProperty("timezone")]
        public long Timezone { get; set; }

        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }

        [JsonProperty("sunset")]
        public long Sunset { get; set; }
    }

    public class ListInfoAboutWeather
    {
        [JsonProperty("dt")]
        public long Dt { get; set; }

        [JsonProperty("main")]
        public Main Main { get; set; }

        [JsonProperty("weather")]
        public Weather[] Weather { get; set; }

        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("visibility")]
        public long Visibility { get; set; }

        [JsonProperty("pop")]
        public double Pop { get; set; }

        [JsonProperty("sys")]
        public Sys Sys { get; set; }

        [JsonProperty("dt_txt")]
        public DateTimeOffset DtTxt { get; set; }

    }
    public partial class Coord
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }
    }

    public class Main
    {
        [JsonProperty("cnt")]
        public double temp { get; set; }

        [JsonProperty("cnt")]
        public double feels_like { get; set; }

        [JsonProperty("cnt")]
        public double temp_min { get; set; }

        [JsonProperty("cnt")]
        public double temp_max { get; set; }

        [JsonProperty("cnt")]
        public int pressure { get; set; }

        [JsonProperty("cnt")]
        public int sea_level { get; set; }

        [JsonProperty("cnt")]
        public int grnd_level { get; set; }

        [JsonProperty("cnt")]
        public int humidity { get; set; }

        [JsonProperty("cnt")]
        public double temp_kf { get; set; }

    }
    public class Weather
    {
        [JsonProperty("cnt")]
        public int id { get; set; }

        [JsonProperty("cnt")]
        public string main { get; set; }

        [JsonProperty("cnt")]
        public string description { get; set; }

        [JsonProperty("cnt")]
        public string icon { get; set; }

    }
    public partial class Clouds
    {
        [JsonProperty("all")]
        public long All { get; set; }
    }

    public partial class Wind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("deg")]
        public long Deg { get; set; }

        [JsonProperty("gust")]
        public double Gust { get; set; }
    }

    public partial class Sys
    {
        [JsonProperty("pod")]
        public string Pod { get; set; }
    }
}
