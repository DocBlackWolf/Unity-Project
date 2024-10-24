using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;  // Required for SceneAsset
#endif

public class SC_SceneLoader : MonoBehaviour
{
    [Header("Configuracion")]
    [Tooltip("The size of the square area.")]
    public Vector2 areaSize = new Vector2(5f, 5f);  // Size of the square area

#if UNITY_EDITOR
    [Tooltip("The scene to load when the player enters the area.")]
    public SceneAsset sceneToLoadAsset;  // SceneAsset to pick the scene from the editor
#endif

    [HideInInspector]
    public string sceneToLoad;  // This will store the scene's name to be loaded

    private void OnDrawGizmosSelected()
    {
        // Draw the square area in the Scene view for visualization
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(areaSize.x, areaSize.y, 1));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player entered the area
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the area. Loading scene: " + sceneToLoad);
            LoadNextScene();
        }
    }

    // Method to load the next scene
    private void LoadNextScene()
    {
        // Check if the scene name is valid
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene name is empty or invalid!");
        }
    }

    private void OnValidate()
    {
        // Automatically set the sceneToLoad string to match the scene selected in the editor
#if UNITY_EDITOR
        if (sceneToLoadAsset != null)
        {
            sceneToLoad = sceneToLoadAsset.name;
        }
        else
        {
            sceneToLoad = "";
        }
#endif
    }
}
