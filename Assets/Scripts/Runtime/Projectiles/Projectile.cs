using UnityEngine;

/// <summary>
/// Projectile that uses strategy patterns for movement and damage.
/// Refactored to follow Open/Closed Principle - extensible without modification.
/// </summary>
public class Projectile : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private ProjectileSO projectileData;

    private IProjectileMovementStrategy movementStrategy;
    private IDamageDealer damageDealer;
    private Vector3 targetPosition;

    //==================================================================================================================================
    private void Awake()
    {
        if (projectileData == null)
        {
            Debug.LogError("ProjectileSO is not assigned!", this);
            Destroy(gameObject);
            return;
        }

        // Initialize strategies based on configuration
        InitializeStrategies();
    }

    //==================================================================================================================================
    private void Update()
    {
        if (movementStrategy != null)
        {
            movementStrategy.Move(transform, targetPosition, projectileData.Speed, Time.deltaTime);

            // Check if reached target (for homing projectiles)
            if (movementStrategy.HasReachedTarget(transform.position, targetPosition))
            {
                Explode();
                Destroy(gameObject);
            }
        }
    }

    //==================================================================================================================================
    private void OnTriggerEnter(Collider other)
    {
        // Skip player collisions
        if (other.CompareTag("Player"))
            return;

        // Deal damage based on strategy
        if (damageDealer != null)
        {
            damageDealer.DealDamage(transform.position, projectileData.Damage, other);
        }

        // Explode and destroy
        Explode();
        Destroy(gameObject);
    }

    //==================================================================================================================================
    /// <summary>
    /// Initialize movement and damage strategies based on projectile configuration.
    /// </summary>
    private void InitializeStrategies()
    {
        // Select movement strategy
        if (projectileData.IsAOE)
        {
            movementStrategy = new HomingMovementStrategy();
            damageDealer = new AOEDamageDealer(projectileData.AOERadius);
        }
        else
        {
            movementStrategy = new ForwardMovementStrategy();
            damageDealer = new SingleTargetDamageDealer();
        }
    }

    //==================================================================================================================================
    /// <summary>
    /// Set the target position for the projectile.
    /// </summary>
    /// <param name="target">Target transform.</param>
    public void SetTargetPos(Transform target)
    {
        if (target != null)
        {
            targetPosition = target.position;
        }
        else
        {
            // Fallback: shoot forward
            targetPosition = transform.position + transform.forward * 100f;
        }
    }

    //==================================================================================================================================
    /// <summary>
    /// Instantiate explosion effect at current position.
    /// </summary>
    private void Explode()
    {
        if (projectileData.ExplosionFX != null)
        {
            Instantiate(projectileData.ExplosionFX, transform.position, transform.rotation);
        }
    }
}
