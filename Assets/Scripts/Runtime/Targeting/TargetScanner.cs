using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TheGamerUrso
{
    /// <summary>
    /// Scans for potential targets within a specified radius.
    /// Single Responsibility: Target detection only.
    /// </summary>
    public class TargetScanner
    {
        public float SearchRadius { get; private set; }
        public LayerMask TargetMask { get; private set; }

        public TargetScanner(float searchRadius, LayerMask targetMask)
        {
            SearchRadius = searchRadius;
            TargetMask = targetMask;
        }

        /// <summary>
        /// Find all potential targets within the search radius.
        /// </summary>
        /// <param name="origin">Position to search from.</param>
        /// <returns>List of potential target transforms.</returns>
        public List<Transform> ScanForTargets(Vector3 origin)
        {
            Collider[] hitColliders = Physics.OverlapSphere(origin, SearchRadius, TargetMask);
            return hitColliders.Select(c => c.transform).ToList();
        }
    }
}
