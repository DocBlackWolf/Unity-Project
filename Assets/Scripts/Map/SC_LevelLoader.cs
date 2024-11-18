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
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Update()
    {
        // Check for Escape key press to reset the game to scene index 0
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetGame();
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

        // Trigger the "OnLevelLoading" event
        OnLevelLoading?.Invoke();

        yield return new WaitForSeconds(1); // Simulate loading delay

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        loading = false;

        // Trigger the "OnLevelLoaded" event
        OnLevelLoaded?.Invoke();
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        loading = true;

        // Trigger the "OnLevelLoading" event
        OnLevelLoading?.Invoke();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        loading = false;

        // Trigger the "OnLevelLoaded" event
        OnLevelLoaded?.Invoke();
    }

    private void ResetGame()
    {
        loading = true;

        // Trigger the "OnLevelLoading" event
        OnLevelLoading?.Invoke();

        // Load scene index 0 to reset the game
        SceneManager.LoadScene(0);

        loading = false;

        // Trigger the "OnLevelLoaded" event
        OnLevelLoaded?.Invoke();

        // Destroy this object after loading the scene
        Destroy(gameObject);
    }

    public void ResetLevel()
    {
        StartCoroutine(StartResetLevel());
    }

    private IEnumerator StartResetLevel()
    {

        loading = true;

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        loading = false;

        Debug.Log("Level reset!");
    }
}
