using UnityEngine;

/// <summary>
/// Strategy interface for dealing damage.
/// Allows different damage types without modifying the Projectile class (OCP).
/// </summary>
public interface IDamageDealer
{
    /// <summary>
    /// Deal damage based on the strategy's implementation.
    /// </summary>
    /// <param name="impactPosition">Position where damage should be dealt.</param>
    /// <param name="damageAmount">Amount of damage to deal.</param>
    /// <param name="impactedCollider">The collider that was hit (for single-target).</param>
    void DealDamage(Vector3 impactPosition, float damageAmount, Collider impactedCollider = null);
}
