using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PauseMenu : MonoBehaviour
{

    public static bool GameIsPause = true;

    public GameObject pauseMenuUI;
    private void Start()
    {
        Resume(); 
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

     public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPause = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPause = true;
    }
}
