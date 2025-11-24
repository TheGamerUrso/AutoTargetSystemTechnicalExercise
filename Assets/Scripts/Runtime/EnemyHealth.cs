using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;
    public bool IsDead { get; private set; } = false;
    //==================================================================================================================================
    private void Awake()
    {
        maxHealth = Random.Range(50, 150);
        currentHealth = maxHealth;
    }
    //==================================================================================================================================
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    //==================================================================================================================================
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    //==================================================================================================================================
    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth * 100f;
    }
    //==================================================================================================================================
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            IsDead = true;
            currentHealth = 0;
            gameObject.SetActive(false);
        }
    }
}