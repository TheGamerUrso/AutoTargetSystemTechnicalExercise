using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private ProjectileSO projectileData;
    private Rigidbody rigid;
    private Vector3 currentTargetPos;
    //==================================================================================================================================
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        if(rigid == null )
        {
            Debug.LogWarning("Rigidbody component is missing.",gameObject);
        }
    }
    //==================================================================================================================================
    private void Update() => Move();
    //==================================================================================================================================
    // Move projectile
    public void Move()
    {
        Debug.DrawRay(transform.position, currentTargetPos - transform.position , Color.yellow);

        // AOE projectile move toward target position
        if (!projectileData.IsAOE)
        {
            // Non-AOE projectile move forward
            transform.position += transform.forward *
                   projectileData.Speed * Time.deltaTime;

            // Destroy if out of screen bounds
            var worldToScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
            if (worldToScreenPoint.x <= 0 || worldToScreenPoint.y <= 0 || worldToScreenPoint.x >= Screen.width || worldToScreenPoint.y >= Screen.height)
            {
                Destroy(gameObject);
            }
        }

        // Check if reached target position
        if (Vector3.Distance(transform.position, currentTargetPos) < 0.1f)
        {
            Explode();
        }
    }
    //==================================================================================================================================
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;

        // Deal damage to damagable objects
        var damagable = other.transform.GetComponentInParent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(10f);
        }
        // Explode on impact if configured
        if (projectileData.IsAOE)
        {
            DoAOEDamage();
        }
        //Destroy projectile
        Explode();
        Destroy(gameObject);
    }
    //==================================================================================================================================
    // Set target position for AOE projectiles
    public void SetTargetPos(Transform target)
    {
        currentTargetPos = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
    }
    //==================================================================================================================================
    // Instantiate explosion effect
    public void Explode()
    {
        Instantiate(projectileData.ExplosionFX, transform.position, transform.rotation);
    }
    //==================================================================================================================================
    // Deal AOE damage
    public void DoAOEDamage()
    {
        var col = Physics.OverlapSphere(transform.position, projectileData.AOERadius);
        foreach (var hit in col)
        {
            var damagable = hit.transform.GetComponentInParent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(10f);
            }
        }
    }
}
