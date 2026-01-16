using UnityEngine;

/// <summary>
/// Configuration data for projectiles.
/// </summary>
[CreateAssetMenu(fileName = "New Projectile Data", menuName = "Data/New Projectile Data")]
public class ProjectileSO : ScriptableObject
{
    [Header("Movement")]
    public float Speed = 30f;

    [Header("Damage")]
    public float Damage = 10f;

    [Header("Effects")]
    public GameObject ExplosionFX;
    public bool ExplodeOnImpact = true;

    [Header("Area of Effect")]
    public bool IsAOE = false;
    public float AOERadius = 5f;
}
