using UnityEngine;

public class ExplodeEffect : MonoBehaviour
{
    [SerializeField] private Transform exposionTransform;
    [SerializeField] private int maxExplosionRange = 0;
    [SerializeField] private float explosionSpeed = 5.0f;
    private float explosionTimer = 0.0f;
    //==================================================================================================================================
    private void Update()
    {
        if (exposionTransform == null)
        {
            Debug.LogWarning("Explosion Transform is not assigned.", gameObject);
            return;
        }

        explosionTimer += Time.deltaTime * explosionSpeed;
        exposionTransform.localScale =
            Vector3.Lerp(exposionTransform.localScale, new Vector3(maxExplosionRange, maxExplosionRange, maxExplosionRange),
            explosionTimer);

        if (explosionTimer >= 1)
        {
            Destroy(gameObject);
        }
    }
}
