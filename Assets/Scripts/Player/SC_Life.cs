using UnityEngine;
using System;

public class SC_Life : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float maxHealth = 5f;
    private float health;
    private bool hasDied = false;

    public static event Action<float> OnHealthChanged;
    public static event Action OnPlayerDied;
    public static event Action<float> OnPlayerDamaged;

    private void Start()
    {
        health = maxHealth;
        BroadcastHealthChange();
    }

    private void OnEnable()
    {
        OnPlayerDamaged += ModifyHealth; // Subscribe to damage event
    }

    private void OnDisable()
    {
        OnPlayerDamaged -= ModifyHealth; // Unsubscribe from damage event
    }

    public void ModifyHealth(float damage)
    {
        health -= damage;
        Debug.Log($"Damage received: {damage}, Current HP: {health}"); // Debug confirmation

        BroadcastHealthChange();

        if (!IsAlive() && !hasDied)
        {
            Debug.Log("Health reached zero. Triggering death."); // Debug confirmation
            TriggerDeath();
            hasDied = true;
        }
    }

    private void BroadcastHealthChange()
    {
        OnHealthChanged?.Invoke(health); // Notify listeners
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    private void TriggerDeath()
    {
        Debug.Log("TriggerDeath called."); // Debug confirmation
        OnPlayerDied?.Invoke();

        if (SC_LevelLoader.instance != null)
        {
            SC_LevelLoader.instance.ResetLevel();
        }
        else
        {
            Debug.LogError("SC_LevelLoader instance is null. Cannot reset level."); // Debug error
        }
    }

    public static void DamagePlayer(float damageAmount)
    {
        Debug.Log($"Damage triggered: {damageAmount}"); // Debug confirmation
        OnPlayerDamaged?.Invoke(damageAmount);
    }
}
