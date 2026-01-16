using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Attack priority rule based on target health.
/// Refactored to depend on IHealth interface instead of concrete EnemyHealth (DIP).
/// Supports chaining with other rules.
/// </summary>
[CreateAssetMenu(fileName = "New By Low Health Rule", menuName = "Data/New By Low Health Rule")]
public class LowHealthRule : AttackPriorityRuleSO
{
    public bool IsDescendingOrder;

    //==================================================================================================================================
    public override IOrderedEnumerable<Transform> Apply(GameObject attacker, List<Transform> targetsList)
    {
        // Sort targets based on their current health using IHealth interface
        if (!IsDescendingOrder)
        {
            return targetsList.OrderBy(e => e.GetComponentInParent<IHealth>()?.CurrentHealth ?? float.MaxValue);
        }
        else
        {
            return targetsList.OrderByDescending(e => e.GetComponentInParent<IHealth>()?.CurrentHealth ?? 0f);
        }
    }

    //==================================================================================================================================
    public override IOrderedEnumerable<Transform> ApplyThen(GameObject attacker, IOrderedEnumerable<Transform> orderedTargets)
    {
        // Chain sorting with ThenBy for multi-level sorting
        if (!IsDescendingOrder)
        {
            return orderedTargets.ThenBy(e => e.GetComponentInParent<IHealth>()?.CurrentHealth ?? float.MaxValue);
        }
        else
        {
            return orderedTargets.ThenByDescending(e => e.GetComponentInParent<IHealth>()?.CurrentHealth ?? 0f);
        }
    }
}
