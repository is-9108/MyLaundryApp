namespace MyLaundryApp.Application.UseCases.GetHello;

public class GetHelloUseCase : IGetHelloUseCase
{
    public Task<GetHelloResponse> ExecuteAsync(GetHelloRequest request, CancellationToken cancellationToken = default)
    {
        var name = string.IsNullOrWhiteSpace(request.Name) ? "World" : request.Name.Trim();
        var response = new GetHelloResponse(Message: $"Hello, {name}!");
        return Task.FromResult(response);
    }
}
