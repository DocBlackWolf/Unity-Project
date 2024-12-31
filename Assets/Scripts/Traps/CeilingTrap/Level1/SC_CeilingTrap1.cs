using System.Collections;
using UnityEngine;

public class SC_CeilingTrap1 : MonoBehaviour
{
    [SerializeField] private bool isCooldown = false;      // Cooldown flag to prevent constant damage
    [SerializeField] private bool playerInside = false;    // To check if the player is inside the trap
    private Collider2D playerInRange;     // Store the player when they enter the trap's trigger

    [SerializeField] private float damageAmount = -1f;      // Amount of damage the trap inflicts (negative to reduce life)
    [SerializeField] private float cooldownTime = 2f;       // Time between damage applications
    [SerializeField] private float damageDelay = 1f;        // Delay before damage is applied after entering the trap
    [SerializeField] private float graceTime = 0.5f;        // Time that the player has to escape even after the trap has been activated

    public bool IsPlayerInside()
    {
        return playerInside;
    }

    public bool IsCooldown()
    {
        return isCooldown;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCooldown)
        {
            playerInside = true;
            playerInRange = other;  // Store the player reference
            StartCoroutine(ApplyDamageWithDelayAndCooldown());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            playerInRange = null;
        }
    }

    private IEnumerator ApplyDamageWithDelayAndCooldown()
    {
        // Wait for the damage delay
        yield return new WaitForSeconds(damageDelay);

        // Start cooldown immediately after applying damage
        isCooldown = true;

        // Wait for grace time before resetting the cooldown
        yield return new WaitForSeconds(graceTime);

        // Apply damage only if the player is still inside the trap
        if (playerInRange != null && playerInside)
        {
            Debug.Log("Triggering damage from ceiling trap");  // Debug message to verify trap triggers

            // Trigger the damage event
            SC_Life.DamagePlayer(damageAmount);
        }

        // After grace time, start cooldown and prevent multiple hits during this time
        yield return new WaitForSeconds(cooldownTime);

        // Reset cooldown after waiting for cooldown time
        isCooldown = false;
    }
}
