using UnityEngine;

/// <summary>
/// Visualizes the current target with a laser line.
/// Refactored to use ITargetProvider interface (DIP).
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class LaserVisual : MonoBehaviour
{
    private ITargetProvider targetProvider;
    private Transform currentTarget;
    private LineRenderer lineRenderer;
    private float searchRadius;

    //==================================================================================================================================
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    //==================================================================================================================================
    private void Start()
    {
        // Get target provider from ServiceLocator (Dependency Inversion)
        targetProvider = ServiceLocator.Get<ITargetProvider>();

        if (targetProvider == null)
        {
            Debug.LogWarning("ITargetProvider not found in ServiceLocator. LaserVisual will not function.", this);
        }
        else
        {
            targetProvider.OnTargetChanged += OnTargetChanged;

            // Get SearchRadius if AutoTargetSystem is available
            if (targetProvider is AutoTargetSystem autoTargetSystem)
            {
                searchRadius = autoTargetSystem.SearchRadius;
            }
        }
    }

    //==================================================================================================================================
    private void OnDestroy()
    {
        if (targetProvider != null)
        {
            targetProvider.OnTargetChanged -= OnTargetChanged;
        }
    }

    //==================================================================================================================================
    private void Update()
    {
        UpdateLinePositions();
    }

    //==================================================================================================================================
    /// <summary>
    /// Called when the target changes.
    /// </summary>
    private void OnTargetChanged(Transform newTarget)
    {
        currentTarget = newTarget;
        lineRenderer.enabled = currentTarget != null;
    }

    //==================================================================================================================================
    /// <summary>
    /// Update the line renderer positions to point at the current target.
    /// </summary>
    private void UpdateLinePositions()
    {
        if (currentTarget == null)
        {
            lineRenderer.enabled = false;
            return;
        }

        lineRenderer.enabled = true;

        Vector3 startPosition = transform.position + Vector3.up;
        Vector3 endPosition = currentTarget.position;

        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
    }
}
