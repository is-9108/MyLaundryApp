using MyLaundryApp.Application.Services;

namespace MyLaundryApp.Infrastructure.Services;

/// <summary>
/// システム時刻のアダプター実装
/// </summary>
public class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
