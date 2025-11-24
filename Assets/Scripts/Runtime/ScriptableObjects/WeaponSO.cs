using UnityEngine;
// Weapon ScriptableObject to define weapon properties
[CreateAssetMenu(fileName = "New Weapon Data", menuName = "Data/New Weapon Data")]
public class WeaponSO : ScriptableObject
{
    public WeaponTypeEnum WeaponType; // Type of the weapon
    public float Damage = 10f; // Damage per hit
    public float Range = 50f; // Maximum range of the weapon
    public float FireRate = 1f; // Shots per second
    public Projectile ProjectilePrefab; // Projectile prefab to shoot
}
