using UnityEngine;
using UnityEngine.UI;

namespace TheGamerUrso
{
    public class HealthUI : MonoBehaviour
    {
        public EnemyHealth enemyHealth;
        public Image image;
        private Color fullHealth = Color.green;
        private Color zeroHealth = Color.red;
        void Update()
        {
            var healthPercent = enemyHealth.HealthPercentage / 100;
            image.fillAmount = healthPercent;
            image.color = Color.Lerp(zeroHealth, fullHealth, healthPercent);
        }
    }
}
