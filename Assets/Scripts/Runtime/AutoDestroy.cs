using UnityEngine;
// AutoDestroy component to destroy the GameObject after a set time
public class AutoDestroy : MonoBehaviour
{

    [Header("Configuration")]
    [SerializeField]
    private float destroyTime = 1f;
    //==================================================================================================================================
    void Awake() => Destroy(gameObject, destroyTime);
}
