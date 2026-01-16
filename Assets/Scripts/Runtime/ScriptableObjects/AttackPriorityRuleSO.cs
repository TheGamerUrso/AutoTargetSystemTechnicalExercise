using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Base class for attack priority rules.
/// Supports chaining multiple rules using OrderBy/ThenBy pattern.
/// </summary>
public abstract class AttackPriorityRuleSO : ScriptableObject
{
    /// <summary>
    /// Apply this rule as the first ordering rule (uses OrderBy).
    /// </summary>
    public abstract IOrderedEnumerable<Transform> Apply(GameObject attacker, List<Transform> targetsList);

    /// <summary>
    /// Apply this rule as a subsequent ordering rule (uses ThenBy).
    /// Override this to support multi-level sorting.
    /// </summary>
    public abstract IOrderedEnumerable<Transform> ApplyThen(GameObject attacker, IOrderedEnumerable<Transform> orderedTargets);
}
