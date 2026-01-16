using UnityEngine;

/// <summary>
/// Movement strategy for projectiles that move straight forward.
/// Includes screen bounds checking.
/// </summary>
public class ForwardMovementStrategy : IProjectileMovementStrategy
{
    private readonly float reachedThreshold = 0.1f;

    public void Move(Transform projectileTransform, Vector3 targetPosition, float speed, float deltaTime)
    {
        // Move forward in the projectile's facing direction
        projectileTransform.position += projectileTransform.forward * speed * deltaTime;

        // Check and destroy if out of screen bounds
        if (IsOutOfScreenBounds(projectileTransform.position))
        {
            Object.Destroy(projectileTransform.gameObject);
        }
    }

    public bool HasReachedTarget(Vector3 currentPosition, Vector3 targetPosition)
    {
        // Forward projectiles don't actually track to target position
        // This is handled by collision instead
        return false;
    }

    /// <summary>
    /// Check if the projectile is out of screen bounds.
    /// </summary>
    private bool IsOutOfScreenBounds(Vector3 worldPosition)
    {
        if (Camera.main == null)
            return false;

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(worldPosition);

        return screenPoint.x <= 0f || 
               screenPoint.y <= 0f || 
               screenPoint.x >= Screen.width || 
               screenPoint.y >= Screen.height;
    }
}
