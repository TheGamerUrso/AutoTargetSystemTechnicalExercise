using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Attack priority rule based on distance from attacker.
/// Supports chaining with other rules.
/// </summary>
[CreateAssetMenu(fileName = "New By Distance Rule", menuName = "Data/New By Distance Rule")]
public class DistanceRule : AttackPriorityRuleSO
{
    public bool IsDescendingOrder;

    //==================================================================================================================================
    public override IOrderedEnumerable<Transform> Apply(GameObject attacker, List<Transform> targetsList)
    {
        if (!IsDescendingOrder)
        {
            return targetsList.OrderBy(e => Vector3.Distance(attacker.transform.position, e.position));
        }
        else
        {
            return targetsList.OrderByDescending(e => Vector3.Distance(attacker.transform.position, e.position));
        }
    }

    //==================================================================================================================================
    public override IOrderedEnumerable<Transform> ApplyThen(GameObject attacker, IOrderedEnumerable<Transform> orderedTargets)
    {
        if (!IsDescendingOrder)
        {
            return orderedTargets.ThenBy(e => Vector3.Distance(attacker.transform.position, e.position));
        }
        else
        {
            return orderedTargets.ThenByDescending(e => Vector3.Distance(attacker.transform.position, e.position));
        }
    }
}
