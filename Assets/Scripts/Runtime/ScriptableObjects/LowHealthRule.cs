using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Attack priority rule based on low health of targets
[CreateAssetMenu(fileName = "New By Low Health Rule", menuName = "Data/New By Low Health Rule")]
public class LowHealthRule : AttackPriorityRuleSO
{
    // If true, sorts in descending order
    public bool IsDescendingOrder;
    //==================================================================================================================================
    public override IOrderedEnumerable<Transform> Apply(GameObject attacker, List<Transform> targetsList)
    {
        // Sort targets based on their current health
        if (!IsDescendingOrder)
        {
            return targetsList.OrderBy(e => e.GetComponentInParent<EnemyHealth>().GetCurrentHealth());
        }
        else
        {
            return targetsList.OrderByDescending(e => e.GetComponentInParent<EnemyHealth>().GetCurrentHealth());
        }
    }
}
