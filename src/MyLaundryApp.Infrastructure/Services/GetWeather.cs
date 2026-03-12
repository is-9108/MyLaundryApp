using MyLaundryApp.Application.Services;
using System.Net.Http.Json;
using System.Text.Json;

namespace MyLaundryApp.Infrastructure.Services
{
    public class GetWeather : IWeatherService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string _WeatherApiUrl = "https://api.openweathermap.org/data/2.5/forecast";
        private readonly string _apiKey;
        public GetWeather(string? apiKey = null)
        {
            _apiKey = apiKey ?? Environment.GetEnvironmentVariable("OPENWEATHERMAP_API_KEY") ?? throw new ArgumentNullException("API key is required");
        }
        public async Task<bool> GetWeatherAsync(CancellationToken cancellationToken = default)
        {
            var url = $"{_WeatherApiUrl}?lat=35.651162&lon=139.451913&units=metric&lang=ja&appid={_apiKey}";
            var response = await _httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var forecast = await response.Content.ReadFromJsonAsync<ForecastResponse>(options, cancellationToken).ConfigureAwait(false)
                ?? throw new InvalidOperationException("天気予報の取得に失敗しました。");
            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            foreach(var item in forecast.List)
            {
                var itemDate = DateTimeOffset.FromUnixTimeSeconds(item.Dt).UtcDateTime.Date;
                if (itemDate != today && itemDate != tomorrow)
                    continue;

                if (item.Weather.Any(IsRainCondition))
                    return true;
            }

            return false;
        }
        private static bool IsRainCondition(WeatherItem w) =>
            (w.Id >= 200 && w.Id < 300) ||
            (w.Id >= 300 && w.Id < 400) ||
            (w.Id >= 500 && w.Id < 600);
    }
}
