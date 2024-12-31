using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_LevelReset : MonoBehaviour
{
    // This method reloads the current active scene
    public void ResetLevel()
    {
        // Get the active scene's build index and reload it
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Level reset!");
    }
}
