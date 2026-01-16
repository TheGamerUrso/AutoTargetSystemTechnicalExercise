using UnityEngine;

/// <summary>
/// Deals damage to a single target on impact.
/// </summary>
public class SingleTargetDamageDealer : IDamageDealer
{
    public void DealDamage(Vector3 impactPosition, float damageAmount, Collider impactedCollider = null)
    {
        if (impactedCollider == null)
            return;

        // Try to find IDamageable component
        var damageable = impactedCollider.GetComponentInParent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damageAmount);
        }
    }
}
