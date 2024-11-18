using System.Collections;
using UnityEngine;
using System;

public class DamageArea : MonoBehaviour
{
    [Header("Damage Configuration")]
    public float damageAmount = 1f;      // Amount of damage to apply
    public float damageInterval = 1f;    // Time between damage applications
    public LayerMask playerLayer;        // Layer mask to filter the player

    private bool isDamaging = false;      // Flag to indicate if the player is in range for damage
    private Collider2D playerInRange;     // Reference to the player in range

    // Declare an event for player damage (no need to define this here, as it will be used from SC_Life)

    private IEnumerator DamagePlayer()
    {
        while (isDamaging)
        {
            if (playerInRange != null)
            {
                // Use the DamagePlayer method from SC_Life to apply damage
                SC_Life.DamagePlayer(damageAmount);  // Trigger damage through SC_Life
                Debug.Log("Player damaged: " + damageAmount);
            }

            // Wait for the specified damage interval before applying damage again
            yield return new WaitForSeconds(damageInterval);
        }
    }

    // Trigger entered: start damaging the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)  // Check if the object is on the playerLayer
        {
            Debug.Log("Player entered the damage area.");
            isDamaging = true;
            playerInRange = collision;
            StartCoroutine(DamagePlayer());
        }
    }

    // Trigger exited: stop damaging the player
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)  // Check if the object is on the playerLayer
        {
            Debug.Log("Player exited the damage area.");
            isDamaging = false;
            playerInRange = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);  // Visualize the damage area
    }
}
