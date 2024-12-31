using UnityEngine;
using UnityEngine.SceneManagement; 

public class DontDestroySingleton : MonoBehaviour
{
    public static DontDestroySingleton Instance { get; private set; }

    private void Awake()
    {

        // Ensure the singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
            return;
        }
    }
}
