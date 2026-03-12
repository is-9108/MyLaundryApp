using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Microsoft.Extensions.DependencyInjection;
using MyLaundryApp.Application.Services;
using MyLaundryApp.Domain.Entities;
using MyLaundryApp.Infrastructure.Services;

namespace MyLaundryApp.Presentation;

public class Function
{
    private readonly IServiceProvider _serviceProvider;
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public Function()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Infrastructure: IWeatherService → GetWeather（APIキーは環境変数 OPENWEATHERMAP_API_KEY から取得）
        services.AddSingleton<IWeatherService>(_ => new GetWeather());
    }

    /// <summary>
    /// Lambda エントリポイント
    /// </summary>
    [LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]
    public async Task<string> FunctionHandler(JsonElement request, ILambdaContext context)
    {
        context.Logger.LogLine("Function invoked.");
        var weatherService = _serviceProvider.GetRequiredService<IWeatherService>();
        bool hasRain = await weatherService.GetWeatherAsync();
        var laundry = new Laundry(hasRain);
        return laundry.Message;
    }
}
