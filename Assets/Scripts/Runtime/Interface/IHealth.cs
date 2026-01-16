/// <summary>
/// Provides read-only access to health information.
/// Separated from IDamageable to follow Interface Segregation Principle.
/// </summary>
public interface IHealth
{
    /// <summary>
    /// Current health value.
    /// </summary>
    float CurrentHealth { get; }

    /// <summary>
    /// Maximum health value.
    /// </summary>
    float MaxHealth { get; }

    /// <summary>
    /// Health as a percentage (0-100).
    /// </summary>
    float HealthPercentage { get; }
}
