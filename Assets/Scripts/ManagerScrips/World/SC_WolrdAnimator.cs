using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WorldAnimator : MonoBehaviour
{
    [SerializeField] private Animator miAnimator;  // Assign Animator from any GameObject in Inspector
    [SerializeField] private SC_LevelLoader loader; // Assign SC_LevelLoader from any GameObject in Inspector
    private string currentState;

    void OnEnable()
    {
        // Check if the components are assigned in the Inspector
        if (miAnimator == null)
        {
            Debug.LogError("Animator component is not assigned in the Inspector.");
        }
        if (loader == null)
        {
            Debug.LogError("SC_LevelLoader component is not assigned in the Inspector.");
        }
    }



    void Update()
    {
        if (loader != null && loader.Loaded())
        {
            miAnimator.Play("End");
        }
        else
        {
            miAnimator.Play("Start");
        }

    }
}
