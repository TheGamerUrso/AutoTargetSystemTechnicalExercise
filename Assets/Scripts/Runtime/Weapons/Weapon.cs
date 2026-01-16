using UnityEngine;

/// <summary>
/// Handles shooting projectiles at targets.
/// Refactored to depend on ITargetProvider interface instead of concrete AutoTargetSystem.
/// </summary>
public class Weapon : MonoBehaviour
{
    [Header("Configuration")]
    public WeaponSO weaponData;

    [SerializeField] private float angleThreshold = 30f;
    [SerializeField] private Transform attackPoint;

    private ITargetProvider targetProvider;
    private Transform currentTarget;
    private float nextFireTime = 0f;

    //==================================================================================================================================
    private void Start()
    {
        // Get target provider from ServiceLocator (Dependency Inversion)
        targetProvider = ServiceLocator.Get<ITargetProvider>();

        if (targetProvider == null)
        {
            Debug.LogWarning("ITargetProvider not found in ServiceLocator. Weapon will not function.", this);
        }
        else
        {
            // Subscribe to target changes
            targetProvider.OnTargetChanged += OnTargetChanged;
        }
    }

    //==================================================================================================================================
    private void OnDestroy()
    {
        // Unsubscribe from events
        if (targetProvider != null)
        {
            targetProvider.OnTargetChanged -= OnTargetChanged;
        }
    }

    //==================================================================================================================================
    private void Update()
    {
        if (Time.time >= nextFireTime && currentTarget != null)
        {
            Shoot();
            nextFireTime = Time.time + weaponData.FireRate;
        }
    }

    //==================================================================================================================================
    private void OnValidate()
    {
        // Validate configuration in editor
        if (weaponData != null && weaponData.ProjectilePrefab == null)
        {
            Debug.LogWarning($"WeaponSO '{weaponData.name}' is missing ProjectilePrefab!", this);
        }

        if (attackPoint == null)
        {
            Debug.LogWarning("Attack point is not assigned!", this);
        }
    }

    //==================================================================================================================================
    /// <summary>
    /// Called when the target changes.
    /// </summary>
    private void OnTargetChanged(Transform newTarget)
    {
        currentTarget = newTarget;
    }

    //==================================================================================================================================
    /// <summary>
    /// Instantiate and fire a projectile at the current target.
    /// </summary>
    private void Shoot()
    {
        if (weaponData == null || weaponData.ProjectilePrefab == null || attackPoint == null)
            return;

        var projectile = Instantiate(
            weaponData.ProjectilePrefab,
            attackPoint.position,
            attackPoint.rotation
        );

        projectile.SetTargetPos(currentTarget);
    }
}
