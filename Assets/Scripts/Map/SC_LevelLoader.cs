using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SC_LevelLoader : MonoBehaviour
{
    public static SC_LevelLoader instance;
    private bool Loading = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mark this instance as persistent
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Update()
    {
        // Check for F11 key press to reset the game to scene index 0
        if (Input.GetKeyDown(KeyCode.F11))
        {
            ResetGame();
        }
    }

    public bool Loaded()
    {
        return Loading;
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        Loading = true;
        yield return new WaitForSeconds(1);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Loading = false;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    // Method to reset the game to scene index 0 and destroy itself
    private void ResetGame()
    {
        Loading = true;

        // Load scene index 0 to reset the game
        SceneManager.LoadScene(0);

        // Destroy this object after loading the scene
        Destroy(gameObject);

        Loading = false;
    }
}
