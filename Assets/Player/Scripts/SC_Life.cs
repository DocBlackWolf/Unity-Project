using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_Life : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private float vida = 5f;  // Player's health
    private bool hasDied = false;              // To ensure the script runs only once

    // Method to modify health
    public void ModificarVida(float puntos)
    {
        // Update health
        vida += puntos;

        // Print current health to the console
        Debug.Log("Current HP: " + vida);

        // Check if the player is still alive
        if (!EstasVivo() && !hasDied)
        {
            // Execute death action only once
            TriggerDeath();
            hasDied = true;
        }

        Debug.Log(EstasVivo());
    }

    // Helper method to check if the player is alive
    private bool EstasVivo()
    {
        return vida > 0;
    }

    // Method triggered on death
    private void TriggerDeath()
    {
        Debug.Log("HP reached 0! Resetting level.");

        // Try to find the SC_LevelReset script and call ResetLevel
        SC_LevelReset levelReset = FindObjectOfType<SC_LevelReset>();
        if (levelReset != null)
        {
            levelReset.ResetLevel();
        }
        else
        {
            Debug.LogWarning("No SC_LevelReset script found!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for the "Meta" tag collision
        if (!collision.gameObject.CompareTag("Meta")) { return; }

        Debug.Log("GANASTE");
    }
}
