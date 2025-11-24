using UnityEngine;

// Auto target system is required
[RequireComponent(typeof(AutoTargetSystem))]
[RequireComponent(typeof(LineRenderer))]
public class LaiserVIsual : MonoBehaviour
{
    private AutoTargetSystem autoTargetSystem;
    private Transform target;
    private LineRenderer lineRenderer;
    private float searchRadius;
    //==================================================================================================================================
    private void OnDestroy()
    {
        if (autoTargetSystem != null)
        {
            autoTargetSystem.OnTargetAcquired -= SetTarget;
        }
    }
    //==================================================================================================================================
    void Awake()
    {
        // Get required components
        lineRenderer = GetComponent<LineRenderer>();   
    }
    //==================================================================================================================================
    private void Start()
    {
        autoTargetSystem = GameObject.FindFirstObjectByType<AutoTargetSystem>();
        if (autoTargetSystem == null)
        {
            Debug.LogWarning("AutoTargetSystem not found in scene.", gameObject);
        }
        else
        {
            autoTargetSystem.OnTargetAcquired += SetTarget;
            searchRadius = autoTargetSystem.SearchRadius;
        }
    }
    //==================================================================================================================================
    private void Update()
    {
        if (target == null) return;
        SetLineLositions(target);
    }
    //==================================================================================================================================
    // Attack method to shoot at target
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    //==================================================================================================================================
    public void SetLineActive(bool isActive)
    {
        lineRenderer.enabled = isActive;
    }
    //==================================================================================================================================
    private void SetLineLositions(Transform target)
    {
        // Set line positions 
        var lineToTargetPosition =
            new Vector3[] {
                transform.position + Vector3.up,
                target != null ?
                target.position
                :
                transform.position + transform.forward * searchRadius };
        // Apply positions to line renderer
        lineRenderer.SetPositions(lineToTargetPosition);
    }
}
