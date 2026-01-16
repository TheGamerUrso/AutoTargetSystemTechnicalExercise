/// <summary>
/// Represents an entity that can take damage.
/// Corrected spelling from IDamagable to IDamageable.
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// Apply damage to this entity.
    /// </summary>
    /// <param name="damageAmount">Amount of damage to apply.</param>
    void TakeDamage(float damageAmount);
}
