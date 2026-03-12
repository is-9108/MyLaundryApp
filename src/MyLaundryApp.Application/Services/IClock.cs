namespace MyLaundryApp.Application.Services;

/// <summary>
/// 時刻取得のポート（Infrastructure が実装）
/// </summary>
public interface IClock
{
    DateTime UtcNow { get; }
}
