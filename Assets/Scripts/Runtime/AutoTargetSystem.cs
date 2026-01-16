using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheGamerUrso
{
    public class AutoTargetSystem : MonoBehaviour, ITargetProvider
    {
        [Header("Target Configuration")]
        [SerializeField] private float searchRadius = 50f;
        public float SearchRadius => searchRadius;
        [SerializeField] private LayerMask targetMask;
        [SerializeField] private List<AttackPriorityRuleSO> priorityRules;

        [Header("Update Settings")]
        [SerializeField] private float evaluateTargetInterval = 0.15f;

        [Header("Stability Settings")]
        [Tooltip("Prefer current target unless new target is significantly better. Prevents thrashing.")]
        [SerializeField] private bool useTargetRetention = true;
        [Tooltip("Current target must be this many positions worse to switch (0 = switch immediately)")]
        [SerializeField][Range(0, 5)] private int retentionThreshold = 2;

        // ITargetProvider implementation
        public Transform CurrentTarget { get; private set; }
        public event Action<Transform> OnTargetChanged;

        // Composed components (Dependency on abstractions via composition)
        private TargetScanner targetScanner;
        private LineOfSightValidator lineOfSightValidator;
        private TargetPriorityResolver priorityResolver;

        private float evaluateTargetTimer;
        private Transform currentTarget;
        private Transform previousTarget;

        //==================================================================================================================================
        private void Awake()
        {
            // Initialize components
            targetScanner = new TargetScanner(searchRadius, targetMask);
            lineOfSightValidator = new LineOfSightValidator();
            priorityResolver = new TargetPriorityResolver(priorityRules);

            // Register this as the ITargetProvider service
            ServiceLocator.Register<ITargetProvider>(this);
        }
        //==================================================================================================================================
        private void OnDestroy()
        {
            //Unregister from ServiceLocator
            ServiceLocator.Unregister<ITargetProvider>();
        }

        //==================================================================================================================================
        private void Update()
        {
            evaluateTargetTimer -= Time.deltaTime;

            if (evaluateTargetTimer <= 0f)
            {
                evaluateTargetTimer = evaluateTargetInterval;
                EvaluateTargets();
            }
        }

        //==================================================================================================================================
        /// <summary>
        /// Evaluate and select the best target based on configured rules.
        /// </summary>
        private void EvaluateTargets()
        {
            // Step 1: Scan for potential targets
            List<Transform> potentialTargets = targetScanner.ScanForTargets(transform.position);

            if (potentialTargets.Count == 0)
            {
                SetTarget(null);
                return;
            }

            // Step 2: Filter by line of sight
            List<Transform> validTargets = new List<Transform>();
            Vector3 origin = transform.position + Vector3.up;

            foreach (var target in potentialTargets)
            {
                if (lineOfSightValidator.HasLineOfSight(origin, target))
                {
                    validTargets.Add(target);
                }
            }

            if (validTargets.Count == 0)
            {
                SetTarget(null);
                return;
            }

            // Step 3: Apply priority rules
            var orderedTargets = priorityResolver.ResolveTargetPriority(gameObject, validTargets);

            // Step 4: Select best target with stability logic
            currentTarget = orderedTargets[0];

            // Step 5: Notify if target changed
            if (previousTarget != currentTarget)
            {
                SetTarget(currentTarget);
            }

            previousTarget = currentTarget;
        }
        //==================================================================================================================================
        /// <summary>
        /// Set the current target and notify subscribers.
        /// </summary>
        private void SetTarget(Transform target)
        {
            CurrentTarget = target;
            OnTargetChanged?.Invoke(CurrentTarget);
        }
        //==================================================================================================================================
        private void OnDrawGizmos()
        {
            // Visualize search radius
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, searchRadius);

            // Show found targets
            if (Application.isPlaying && targetScanner != null)
            {
                var targets = targetScanner.ScanForTargets(transform.position);
                Gizmos.color = targets.Count > 0 ? Color.green : Color.red;
                Gizmos.DrawWireSphere(transform.position, searchRadius * 0.95f);
            }
        }
        //==================================================================================================================================
        private void OnValidate()
        {
            if (searchRadius <= 0f)
            {
                Debug.LogWarning("Search radius must be greater than 0!", this);
                searchRadius = 1f;
            }

            if (evaluateTargetInterval <= 0f)
            {
                Debug.LogWarning("Evaluate target interval must be greater than 0!", this);
                evaluateTargetInterval = 0.1f;
            }
        }
    }
}
