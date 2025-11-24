using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Base class for attack priority rules
public abstract class AttackPriorityRuleSO : ScriptableObject
{
    // Evaluate targets and return sorted list based on rule
    public abstract IOrderedEnumerable<Transform> Apply(GameObject attacker, List<Transform> targetsList);
}
