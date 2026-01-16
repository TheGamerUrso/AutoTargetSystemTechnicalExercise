using UnityEngine;

/// <summary>
/// Strategy interface for projectile movement behavior.
/// Allows different movement types without modifying the Projectile class (OCP).
/// </summary>
public interface IProjectileMovementStrategy
{
    /// <summary>
    /// Move the projectile toward its target.
    /// </summary>
    /// <param name="projectileTransform">The projectile's transform.</param>
    /// <param name="targetPosition">The target position to move toward.</param>
    /// <param name="speed">Movement speed.</param>
    /// <param name="deltaTime">Time since last frame.</param>
    void Move(Transform projectileTransform, Vector3 targetPosition, float speed, float deltaTime);

    /// <summary>
    /// Check if the projectile has reached its target.
    /// </summary>
    /// <param name="currentPosition">Current projectile position.</param>
    /// <param name="targetPosition">Target position.</param>
    /// <returns>True if target reached.</returns>
    bool HasReachedTarget(Vector3 currentPosition, Vector3 targetPosition);
}
