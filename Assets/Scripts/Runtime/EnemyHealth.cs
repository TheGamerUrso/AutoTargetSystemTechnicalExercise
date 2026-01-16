using UnityEngine;
namespace TheGamerUrso
{
    /// <summary>
    /// Manages enemy health and damage.
    /// Implements IHealth (for queries) and IDamageable (for damage) separately (ISP).
    /// </summary>
    public class EnemyHealth : MonoBehaviour, IHealth, IDamageable
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float currentHealth;

        public bool IsDead { get; private set; } = false;

        // IHealth implementation
        public float CurrentHealth => currentHealth;
        public float MaxHealth => maxHealth;
        public float HealthPercentage => (currentHealth / maxHealth) * 100f;

        //==================================================================================================================================
        private void Awake()
        {
            maxHealth = Random.Range(50f, 150f);
            currentHealth = maxHealth;
        }

        //==================================================================================================================================
        // IDamageable implementation
        public void TakeDamage(float damageAmount)
        {
            if (IsDead)
                return;

            currentHealth -= damageAmount;

            if (currentHealth <= 0f)
            {
                IsDead = true;
                currentHealth = 0f;
                gameObject.SetActive(false);
            }
        }
    }
}
