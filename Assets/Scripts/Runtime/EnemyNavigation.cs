using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyNavigation : MonoBehaviour
{
    private const float MinTimeBetweenWanders = 2f;
    private const float MaxTimeBetweenWanders = 3f;
    private const float MinWanderDistance = 1f;
    private const float MaxWanderDistance = 6f;
    private const float MovementSpeed = 3f;
    
    private Vector3 wanderAroundPosition;
    private Vector3 wanderPosition;
    private float newWanderPositionTime;
    
    private void Awake()
    {
        wanderAroundPosition = transform.position;
    }

    private void Update()
    {
        if (Time.time >= newWanderPositionTime)
        {
            SetRandomWanderPosition();
        }
        
        MoveTowardsWanderPosition();
    }

    private void MoveTowardsWanderPosition()
    {
        var wanderDirection = (wanderPosition - transform.position).normalized;
        if (wanderDirection.magnitude >= 0.1f)
        {
            transform.position += wanderDirection.normalized * (MovementSpeed * Time.deltaTime);
        }
    }

    private void SetRandomWanderPosition()
    {
        wanderPosition = GetRandomWanderPosition();
        newWanderPositionTime = Time.time + Random.Range(MinTimeBetweenWanders, MaxTimeBetweenWanders);
    }

    private Vector3 GetRandomWanderPosition()
    {
        var wanderOffset = Random.insideUnitSphere.normalized * Random.Range(MinWanderDistance, MaxWanderDistance);
        wanderOffset.y = 0;
        
        return wanderAroundPosition + wanderOffset;
    }
}