using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Microsoft.Extensions.DependencyInjection;
using MyLaundryApp.Application.UseCases.GetHello;
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
        // Application
        services.AddScoped<IGetHelloUseCase, GetHelloUseCase>();
        // Infrastructure
        services.AddSingleton<Application.Services.IClock, SystemClock>();
    }

    /// <summary>
    /// Lambda エントリポイント
    /// </summary>
    [LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]
    public async Task<JsonElement> FunctionHandler(JsonElement request, ILambdaContext context)
    {
        context.Logger.LogLine("Function invoked.");
        var useCase = _serviceProvider.GetRequiredService<IGetHelloUseCase>();
        var name = request.TryGetProperty("name", out var nameProp)
            ? nameProp.GetString()
            : null;
        var response = await useCase.ExecuteAsync(new GetHelloRequest(name));
        return JsonSerializer.SerializeToElement(new { message = response.Message }, JsonOptions);
    }
}
