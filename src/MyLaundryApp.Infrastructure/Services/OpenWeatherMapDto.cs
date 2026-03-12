using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyLaundryApp.Infrastructure.Services
{
    internal class ForecastResponse
    {
        [JsonPropertyName("list")]
        public List<ForecastItem> List { get; set; } = new();
    }

    internal class ForecastItem
    {
        [JsonPropertyName("dt")]
        public long Dt { get; set; }
        [JsonPropertyName("weather")]
        public List<WeatherItem> Weather { get; set; } = new();
    }

    internal class WeatherItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("main")]
        public string Main { get; set; } = string.Empty;
    }
}
