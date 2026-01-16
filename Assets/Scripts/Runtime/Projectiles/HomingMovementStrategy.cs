using UnityEngine;

/// <summary>
/// Movement strategy for projectiles that home toward a specific position.
/// Used for AOE projectiles that need to reach a target location.
/// </summary>
public class HomingMovementStrategy : IProjectileMovementStrategy
{
    private readonly float reachedThreshold = 0.1f;

    public void Move(Transform projectileTransform, Vector3 targetPosition, float speed, float deltaTime)
    {
        // Move toward target position
        Vector3 direction = (targetPosition - projectileTransform.position).normalized;
        projectileTransform.position += direction * speed * deltaTime;

        // Optional: Rotate to face movement direction
        if (direction != Vector3.zero)
        {
            projectileTransform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public bool HasReachedTarget(Vector3 currentPosition, Vector3 targetPosition)
    {
        float distanceSquared = (targetPosition - currentPosition).sqrMagnitude;
        return distanceSquared < (reachedThreshold * reachedThreshold);
    }
}
