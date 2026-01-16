using System.Linq;
using UnityEngine;

/// <summary>
/// Manages weapon switching and activation.
/// Single Responsibility: Weapon lifecycle management only.
/// </summary>
public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private WeaponTypeEnum currentWeaponType = WeaponTypeEnum.Projectile;

    private Weapon currentActiveWeapon;

    private void Awake()
    {
        SetWeaponType(currentWeaponType);
    }

    /// <summary>
    /// Switch to a different weapon type.
    /// </summary>
    /// <param name="weaponType">The weapon type to switch to.</param>
    public void SetWeaponType(WeaponTypeEnum weaponType)
    {
        currentWeaponType = weaponType;

        // Deactivate all weapons
        foreach (var weapon in weapons)
        {
            if (weapon != null)
            {
                weapon.gameObject.SetActive(false);
            }
        }

        // Activate the selected weapon
        currentActiveWeapon = weapons.FirstOrDefault(w => w != null && w.weaponData.WeaponType == currentWeaponType);

        if (currentActiveWeapon != null)
        {
            currentActiveWeapon.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"No weapon found for type {currentWeaponType}", this);
        }
    }

    /// <summary>
    /// Get the currently active weapon.
    /// </summary>
    public Weapon CurrentWeapon => currentActiveWeapon;
}
