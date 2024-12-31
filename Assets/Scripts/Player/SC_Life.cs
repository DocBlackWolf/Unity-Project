using UnityEngine;
using System;

public class SC_Life : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float maxHealth = 5f;
    private float health;
    private bool hasDied = false;

    // Event to notify when health changes
    public static event Action<float> OnHealthChanged;

    // Event to notify when the player dies
    public static event Action OnPlayerDied;

    // Event to listen to for any damage source
    public static event Action<float> OnPlayerDamaged;

    private void Start()
    {
        health = maxHealth;
        BroadcastHealthChange();
    }

    private void OnEnable()
    {
        // Subscribe to the OnPlayerDamaged event
        OnPlayerDamaged += ModifyHealth;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event
        OnPlayerDamaged -= ModifyHealth;
    }

    public void ModifyHealth(float damage)
    {
        health -= damage;
        Debug.Log("Player damage received: " + damage);
        Debug.Log("Current HP: " + health);

        // Notify listeners about health change
        BroadcastHealthChange();

        if (!IsAlive() && !hasDied)
        {
            TriggerDeath();
            hasDied = true;
        }
    }

    private void BroadcastHealthChange()
    {
        // Notify listeners about health change
        OnHealthChanged?.Invoke(health);
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    private void TriggerDeath()
    {
        Debug.Log("Player has died!");
        // Notify listeners about death
        OnPlayerDied?.Invoke();

        // Reset level
        SC_LevelLoader.instance.ResetLevel();
    }

    public static void DamagePlayer(float damageAmount)
    {
        // Trigger the OnPlayerDamaged event
        Debug.Log("Damage triggered: " + damageAmount); // Debug line to ensure it's triggered
        OnPlayerDamaged?.Invoke(damageAmount);
    }
}
