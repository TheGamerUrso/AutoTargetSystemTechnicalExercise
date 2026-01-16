using UnityEngine;
namespace TheGamerUrso
{
    /// <summary>
    /// Validates line of sight between origin and target.
    /// Single Responsibility: Line-of-sight checking only.
    /// </summary>
    public class LineOfSightValidator
    {
        /// <summary>
        /// Check if there is a clear line of sight to the target.
        /// </summary>
        /// <param name="origin">Starting position for the raycast.</param>
        /// <param name="target">Target to check line of sight to.</param>
        /// <returns>True if line of sight is clear and target has IDamageable component.</returns>
        public bool HasLineOfSight(Vector3 origin, Transform target)
        {
            if (target == null)
                return false;

            Vector3 direction = (target.position - origin);
            float distance = direction.magnitude;

            if (Physics.Raycast(origin, direction.normalized, out RaycastHit hit, distance))
            {
                // Check if the hit object has IDamageable interface
                if (hit.transform.GetComponentInParent<IDamageable>() != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
