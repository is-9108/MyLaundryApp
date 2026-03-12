namespace MyLaundryApp.Domain.Entities;

/// <summary>
/// ドメインエンティティの基底（クリーンアーキテクチャ Domain 層）
/// </summary>
public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
}
