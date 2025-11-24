using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "Data/New Projectile")]
public class ProjectileSO :ScriptableObject
{
    [Header("Configuration")]
    // Projectile speed
    public float Speed = 30f;
    // Explosion effect prefab
    public GameObject ExplosionFX;
    // Should the projectile explode on impact
    public bool ExplodeOnImpact = true;
    // Is Area of Effect projectile
    public bool IsAOE = false;
    // Area of Effect radius
    public float AOERadius = 5f;
}
