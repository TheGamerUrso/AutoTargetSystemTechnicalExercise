using UnityEngine;
namespace TheGamerUrso
{
    /// <summary>
    /// Handles smooth rotation of a GameObject toward the current target.
    /// Single Responsibility: Player/object rotation only.
    /// </summary>
    public class PlayerRotationController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 5f;

        private ITargetProvider targetProvider;

        private void Start()
        {
            // Get target provider from ServiceLocator
            targetProvider = ServiceLocator.Get<ITargetProvider>();

            if (targetProvider == null)
            {
                Debug.LogWarning("ITargetProvider not found in ServiceLocator. PlayerRotationController will not function.", this);
            }
        }

        private void Update()
        {
            RotateTowardTarget();
        }

        /// <summary>
        /// Smoothly rotate toward the current target.
        /// </summary>
        private void RotateTowardTarget()
        {
            if (targetProvider?.CurrentTarget == null)
                return;

            Vector3 directionToTarget = targetProvider.CurrentTarget.position - transform.position;

            if (directionToTarget.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }
        }
    }
}
