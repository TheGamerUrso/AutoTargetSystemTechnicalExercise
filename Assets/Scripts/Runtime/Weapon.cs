using UnityEngine;

[CreateAssetMenu(fileName = "newAttack", menuName = "New Attack", order = 1)]
// Weapon class to handle shooting projectiles
public class Weapon : MonoBehaviour
{
  
    [Header("Configuration")]
    // Weapon data scriptable object
    public WeaponSO weaponData;
    // Fire point transform
    [SerializeField] private Transform firePoint;
    // Time until next fire
    private float nextFireTime = 0f;
    // Target transform
    private Transform target;
    // Angle threshold to shoot
    [SerializeField] private float angleThreshold = 30f;
    private AutoTargetSystem autoTargetSystem;

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
        if(autoTargetSystem != null)
        {
            // Subscribe to OnTargetAcquired event
            autoTargetSystem.OnTargetAcquired += SetTarget;
        }
    }
    //==================================================================================================================================
    public void Update()
    {
        if (target == null) return;
        if (Time.time >= nextFireTime)
        {     
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
                   firePoint.position,
                   firePoint.rotation);
        projectileTemp.SetTargetPos(target);
    }
}
