using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    [SerializeField] private float destroyAfter = 5f;

    private void Start()
    {
        // Schedule the object to be destroyed after the specified time.
        Destroy(gameObject, destroyAfter);
    }
}
