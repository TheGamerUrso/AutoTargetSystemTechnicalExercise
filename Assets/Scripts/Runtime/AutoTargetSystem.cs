using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AutoTargetSystem : MonoBehaviour
{
    public Action<Transform> OnTargetAcquired;
    public Transform CurrentTarget { get; private set; }
    public float SearchRadius => searchRadius;


    [Header("Configuration")]
    [Header("Target")]
    [SerializeField] private const float evaluateTargetTime = 0.15f;
    [SerializeField] private float searchRadius = 50f;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private List<AttackPriorityRuleSO> criteriaList;
    [Header("Weapoon")]
    [SerializeField] private Weapon[] weaponsList;
    [SerializeField] private WeaponTypeEnum currentWeaponType = WeaponTypeEnum.Projectile;
    [SerializeField] private float rotateSpeed = 5f;

    private Transform previousTarget;
    private List<Transform> validTargetsList = new List<Transform>();

    private Weapon currentActiveWeapon;
    private float evaluateTargetTimer;


    //==================================================================================================================================
    private void Awake()
    {
        // Initialize weapon
        SetWeaponType(currentWeaponType);
    }
    //==================================================================================================================================
    private void Update()
    {
        evaluateTargetTimer -= Time.deltaTime;
        // Evaluate targets
        if (evaluateTargetTimer <= 0)
        {
            evaluateTargetTimer = evaluateTargetTime;
            EvaluateTargets();
        }
        // Rotate toward target
        RotateTowardTarget();
    }
    //==================================================================================================================================
    // Set weapon type
    public void SetWeaponType(WeaponTypeEnum nextWeaponType)
    {
        currentWeaponType = nextWeaponType;
        foreach (var weap in weaponsList)
        {
            weap.gameObject.SetActive(false);
        }
        currentActiveWeapon = weaponsList.First(w => w.weaponData.WeaponType == currentWeaponType);
        currentActiveWeapon.gameObject.SetActive(true);
    }
    //==================================================================================================================================
    // Evaluate targets in radius
    private void EvaluateTargets()
    {
        validTargetsList.Clear();

        // Find targets in radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchRadius, targetMask);
        if (hitColliders.Length == 0)
        {
            SetTarget(null);
            return;
        }

        List<Transform> targets = hitColliders.Select(c => c.transform).ToList();
        // Filter by line of sight
        foreach (var t in targets)
        {        
            if (HasLineOfSight(t))
            {
                validTargetsList.Add(t);
            }
        }

        // Apply critiria
        IOrderedEnumerable<Transform> ordered = null;
        foreach (var rule in criteriaList)
        {
            if (ordered == null)
                ordered = rule.Apply(gameObject, validTargetsList);
            else
                ordered = rule.Apply(gameObject, ordered.ToList());
        }

        if (ordered != null)
            validTargetsList = ordered.ToList();

        // Set current target
        CurrentTarget = validTargetsList.Count > 0 ? validTargetsList[0] : null;

        // Notify target acquired
        if (previousTarget != CurrentTarget)
        {
            SetTarget(CurrentTarget);
        }
        previousTarget = CurrentTarget;
    }
    //==================================================================================================================================
    public void SetTarget(Transform target)
    {
        CurrentTarget = target;
        OnTargetAcquired?.Invoke(CurrentTarget);
    }
    //==================================================================================================================================
    // Check line of sight to target
    private bool HasLineOfSight(Transform target)
    {
        Vector3 origin = transform.position + Vector3.up;
        Vector3 direction = (target.position - origin);
        float distance = direction.magnitude;
        if (Physics.Raycast(origin, direction.normalized, out RaycastHit hit, distance))
        {
            Gizmos.color = Color.green;
            if (hit.transform.GetComponentInParent<IDamagable>() != null)
            {
                return true;
            }
        }
        return false;
    }
    //==================================================================================================================================
    // Rotate toward current target
    private void RotateTowardTarget()
    {
        if (CurrentTarget == null) return;
        // Smoothly rotate toward target
        transform.rotation =
            Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(CurrentTarget.position - transform.position),
            rotateSpeed * Time.deltaTime * 5f);
    }
    //==================================================================================================================================
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, searchRadius);
        var cols = Physics.OverlapSphere(transform.position, searchRadius, targetMask);
        Gizmos.color = cols.Length == 0 ? Color.red : Color.green;
    }
}
