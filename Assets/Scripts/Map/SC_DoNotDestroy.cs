using UnityEngine;

public class DontDestroySingleton : MonoBehaviour
{
    public static DontDestroySingleton Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instance
            return;
        }

        Instance = this; 
        DontDestroyOnLoad(gameObject); 
    }
}
