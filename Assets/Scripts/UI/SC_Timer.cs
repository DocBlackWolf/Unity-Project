using System.Collections;
using UnityEngine;
using TMPro;

public class SC_Timer : MonoBehaviour
{
    [SerializeField] private SC_LevelLoader loader;
    [SerializeField] private TextMeshProUGUI timerText;

    private float elapsedTime;
    private bool isCounting = true;

    private void OnEnable()
    {
        // Subscribe to the level loading events
        SC_LevelLoader.OnLevelLoading += HandleLevelLoading;
        SC_LevelLoader.OnLevelLoaded += HandleLevelLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the level loading events
        SC_LevelLoader.OnLevelLoading -= HandleLevelLoading;
        SC_LevelLoader.OnLevelLoaded -= HandleLevelLoaded;
    }

    private void Update()
    {
        if (isCounting)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }
    }

    private void HandleLevelLoading()
    {
        // Stop counting time when loading starts
        isCounting = false;
    }

    private void HandleLevelLoaded()
    {
        // Resume counting time when loading is done
        isCounting = true;
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
