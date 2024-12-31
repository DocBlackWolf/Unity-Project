using UnityEngine;
using UnityEngine.UI;

public class SC_StaminaBar : MonoBehaviour
{
    public Slider slider;

    private void OnEnable()
    {
        // Subscribe to the OnHealthChanged event
        SC_Life.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event
        SC_Life.OnHealthChanged -= UpdateHealthBar;
    }

    public void UpdateHealthBar(float currentHealth)
    {
        slider.value = currentHealth;
    }

    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }
}
