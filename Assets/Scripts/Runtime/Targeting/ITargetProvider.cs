using System;
using UnityEngine;

/// <summary>
/// Provides access to the current target and notifies when the target changes.
/// This interface allows decoupling of systems that need target information.
/// </summary>
public interface ITargetProvider
{
    /// <summary>
    /// The currently selected target. Null if no valid target exists.
    /// </summary>
    Transform CurrentTarget { get; }

    /// <summary>
    /// Event fired when the current target changes. Passes the new target (can be null).
    /// </summary>
    event Action<Transform> OnTargetChanged;
}
