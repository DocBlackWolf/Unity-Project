using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_LevelLoader : MonoBehaviour
{
    public static SC_LevelLoader instance;

    private bool loading = false;

    // Events
    public delegate void LevelLoadingHandler();
    public static event LevelLoadingHandler OnLevelLoading;
    public static event LevelLoadingHandler OnLevelLoaded;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
            Debug.Log("SC_LevelLoader instance initialized."); // Debug confirmation
        }
        else
        {
            Debug.LogWarning("Duplicate SC_LevelLoader destroyed."); // Debug for duplicates
            Destroy(gameObject);
        }
    }

    public bool Loaded()
    {
        return loading;
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        loading = true;
        OnLevelLoading?.Invoke(); // Trigger loading event
        yield return new WaitForSeconds(1); // Simulate loading delay

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        loading = false;
        OnLevelLoaded?.Invoke(); // Trigger loaded event
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        loading = true;
        OnLevelLoading?.Invoke(); // Trigger loading event

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        loading = false;
        OnLevelLoaded?.Invoke(); // Trigger loaded event
    }

    public void ResetLevel()
    {
        Debug.Log("ResetLevel called."); // Debug confirmation
        StartCoroutine(StartResetLevel());
    }

    private IEnumerator StartResetLevel()
    {
        loading = true;
        yield return new WaitForSeconds(1); // Simulate loading delay

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload current scene
        loading = false;
        Debug.Log("Level reset completed!"); // Debug confirmation
    }
}
