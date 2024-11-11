using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_FinishPoint : MonoBehaviour
{
    private bool levelTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!levelTriggered && collision.CompareTag("Player"))
        {
            levelTriggered = true; 
            SC_LevelLoader.instance.NextLevel();
        }
    }
}
