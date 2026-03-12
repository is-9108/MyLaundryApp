namespace MyLaundryApp.Application.Common;

/// <summary>
/// ユースケースのポート（Application 層）
/// </summary>
public interface IUseCase<in TRequest, TResponse>
{
    Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancellationToken = default);
}
