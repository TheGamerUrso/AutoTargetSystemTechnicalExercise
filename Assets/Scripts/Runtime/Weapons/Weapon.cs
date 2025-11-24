using UnityEngine;

// Weapon class to handle shooting projectiles
public class Weapon : MonoBehaviour
{
    private AutoTargetSystem autoTargetSystem;
    // Weapon data scriptable object
    public WeaponSO weaponData;
    [Header("Configuration")]
    // Angle threshold to shoot
    [SerializeField] private float angleThreshold = 30f;
    // Fire point transform
    [SerializeField] private Transform attackPoint;
    // Time until next fire
    private float nextFireTime = 0f;
    // Target transform
    private Transform target;
    //==================================================================================================================================
    private void OnDestroy()
    {
        if (autoTargetSystem != null)
        {
            // Unsubscribe from OnTargetAcquired event
            autoTargetSystem.OnTargetAcquired -= SetTarget;
        }
    }
    //==================================================================================================================================
    private void Start()
    {
        // Find AutoTargetSystem in scene
        autoTargetSystem = GameObject.FindFirstObjectByType<AutoTargetSystem>();
        if (autoTargetSystem == null)
        {
            Debug.LogWarning("AutoTargetSystem not found in scene.", gameObject);
        }
        else
        {
            // Subscribe to OnTargetAcquired event
            autoTargetSystem.OnTargetAcquired += SetTarget;
        }
    }
    //==================================================================================================================================
    public void Update()
    {
        if (Time.time >= nextFireTime)
        {
            if (target == null) return;
            Shoot();
            nextFireTime = Time.time + weaponData.FireRate;
        }
    }
    //==================================================================================================================================
    // Attack method to shoot at target
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    //==================================================================================================================================
    // Shoot method to instantiate projectile
    public void Shoot()
    {
        var projectileTemp = Instantiate(weaponData.ProjectilePrefab,
                   attackPoint.position,
                   attackPoint.rotation);
        projectileTemp.SetTargetPos(target);
    }
}
