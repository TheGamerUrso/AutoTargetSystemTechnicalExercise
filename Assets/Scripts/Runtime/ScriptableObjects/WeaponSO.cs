using UnityEngine;
// Weapon ScriptableObject to define weapon properties
[CreateAssetMenu(fileName = "New Weapon", menuName = "Data/New Weapon")]
public class WeaponSO : ScriptableObject
{
    public WeaponTypeEnum WeaponType;
    public float Damage = 10f;
    public float Range = 50f;
    public float FireRate = 1f;
    public Projectile ProjectilePrefab;
    public bool IsAOE = false;
}
