using UnityEngine;

/// <summary>
/// Deals area-of-effect damage to all targets within a radius.
/// </summary>
public class AOEDamageDealer : IDamageDealer
{
    private readonly float aoeRadius;

    public AOEDamageDealer(float aoeRadius)
    {
        this.aoeRadius = aoeRadius;
    }

    public void DealDamage(Vector3 impactPosition, float damageAmount, Collider impactedCollider = null)
    {
        // Find all colliders in radius
        Collider[] hitColliders = Physics.OverlapSphere(impactPosition, aoeRadius);

        foreach (var collider in hitColliders)
        {
            // Skip the player or projectile itself
            if (collider.CompareTag("Player") || collider.CompareTag("Projectile"))
                continue;

            var damageable = collider.GetComponentInParent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);
            }
        }
    }
}
