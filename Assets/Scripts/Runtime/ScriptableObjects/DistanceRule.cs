using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New By Distance Rule", menuName = "Data/New By Distance Rule")]
public class DistanceRule : AttackPriorityRuleSO
{
    // If true, sorts in descending order
    public bool IsDescendingOrder;
    //==================================================================================================================================
    // Evaluate targets based on distance from attacker
    public override IOrderedEnumerable<Transform> Apply(GameObject attacker, List<Transform> targetsList)
    {
        if (!IsDescendingOrder)
        {
            return targetsList.OrderBy(e => Vector3.Distance(attacker.transform.position, e.transform.position));
        }
        else
        {
            return targetsList.OrderByDescending(e => Vector3.Distance(attacker.transform.position, e.transform.position));
        }
    }
}
