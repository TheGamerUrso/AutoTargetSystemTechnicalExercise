using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Resolves target priority by applying a list of priority rules.
/// Single Responsibility: Target priority ordering only.
/// Uses OrderBy/ThenBy chaining for proper multi-level sorting.
/// Ensures stable sorting with instance ID tie-breaker.
/// </summary>
public class TargetPriorityResolver
{
    private readonly List<AttackPriorityRuleSO> priorityRules;

    public TargetPriorityResolver(List<AttackPriorityRuleSO> priorityRules)
    {
        this.priorityRules = priorityRules ?? new List<AttackPriorityRuleSO>();
    }

    /// <summary>
    /// Apply all priority rules to the target list and return ordered results.
    /// Uses OrderBy for first rule, ThenBy for subsequent rules to properly chain sorting.
    /// Adds instance ID as final tie-breaker to ensure stable, deterministic sorting.
    /// </summary>
    /// <param name="attacker">The attacking GameObject.</param>
    /// <param name="targets">List of potential targets.</param>
    /// <returns>Ordered list of targets based on priority rules.</returns>
    public List<Transform> ResolveTargetPriority(GameObject attacker, List<Transform> targets)
    {
        if (targets == null || targets.Count == 0)
            return new List<Transform>();

        if (priorityRules.Count == 0)
            return targets;

        IOrderedEnumerable<Transform> ordered = null;
        bool isFirstRule = true;

        foreach (var rule in priorityRules)
        {
            if (rule == null)
                continue;

            if (isFirstRule)
            {
                // First rule uses OrderBy
                ordered = rule.Apply(attacker, targets);
                isFirstRule = false;
            }
            else
            {
                // Subsequent rules use ThenBy to chain sorting
                ordered = rule.ApplyThen(attacker, ordered);
            }
        }

        // CRITICAL: Add instance ID as final tie-breaker for stable sorting
        // This ensures targets with identical priority always sort the same way
        if (ordered != null)
        {
            ordered = ordered.ThenBy(t => t.GetInstanceID());
        }

        return ordered != null ? ordered.ToList() : targets;
    }
}
