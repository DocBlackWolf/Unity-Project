using UnityEngine;
using System;

public class SC_Life : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float health = 5f;
    private bool hasDied = false;

    // Event to notify when the player dies
    public static event Action OnPlayerDied;

    // Event to listen to for any damage source
    public static event Action<float> OnPlayerDamaged;

    private void OnEnable()
    {
        // Subscribe to the OnPlayerDamaged event
        OnPlayerDamaged += ModifyHealth;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event when disabled
        OnPlayerDamaged -= ModifyHealth;
    }

    // Method to modify health
    public void ModifyHealth(float damage)
    {
        health -= damage;
        Debug.Log("Player damage received: " + damage);
        Debug.Log("Current HP: " + health);

        if (!IsAlive() && !hasDied)
        {
            TriggerDeath();
            hasDied = true;
        }
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    private void TriggerDeath()
    {
        Debug.Log("Player has died!");
        // Fire the OnPlayerDied event when the player dies
        OnPlayerDied?.Invoke();

        // Call the SC_LevelLoader instance to reset the level
        SC_LevelLoader.instance.ResetLevel();
    }

    // This method can be called by any damage source to trigger the event
    public static void DamagePlayer(float damageAmount)
    {
        // Trigger the OnPlayerDamaged event
        Debug.Log("Damage triggered: " + damageAmount); // Debug line to ensure it's triggered
        OnPlayerDamaged?.Invoke(damageAmount);
    }
}
