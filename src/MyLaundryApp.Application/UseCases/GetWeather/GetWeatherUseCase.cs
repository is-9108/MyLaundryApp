

namespace MyLaundryApp.Application.UseCases.GetWeather
{
    public class GetWeatherUseCase : IGetWeatherUseCase
    {
        public Task<GetWeatherResponse> JudgeLaundly(GetWeatherRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
