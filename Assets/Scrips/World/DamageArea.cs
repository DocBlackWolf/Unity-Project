using System.Collections;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [Header("Damage Configuration")]
    public float damageAmount = 1f;      // Damage dealt to the player
    public float damageRadius = 5f;      // Radius (or side length if square) of the damage area
    public bool isCircular = true;       // True if the area is circular, false if it's square
    public LayerMask playerLayer;        // Layer mask for detecting the player

    private bool isDamaging = false;     // To track if the player is being damaged

    // Coroutine to damage the player once every second
    private IEnumerator DamagePlayer(Collider2D player)
    {
        while (isDamaging)
        {
            // Assume the player has an SC_Life component to modify health
            SC_Life playerLife = player.GetComponent<SC_Life>();
            if (playerLife != null)
            {
                playerLife.ModificarVida(-damageAmount); // Subtract damage from player's health
                Debug.Log("Player damaged: " + damageAmount);
            }
            else
            {
                Debug.LogWarning("No SC_Life component found on player!");
            }

            // Wait for 1 second before damaging again
            yield return new WaitForSeconds(1f);
        }
    }

    void Update()
    {
        // Check for player in circular or square area based on isCircular flag
        if (isCircular)
        {
            DetectPlayerInCircularArea();
        }
        else
        {
            DetectPlayerInSquareArea();
        }
    }

    void DetectPlayerInCircularArea()
    {
        // Detect players within a circular radius
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, damageRadius, playerLayer);

        if (players.Length > 0)
        {
            Debug.Log("Player detected in circular area.");
            if (!isDamaging)
            {
                isDamaging = true;
                StartCoroutine(DamagePlayer(players[0]));  // Start damaging the player
            }
        }
        else
        {
            isDamaging = false;  // Stop damaging if no player is in range
        }
    }

    void DetectPlayerInSquareArea()
    {
        // Define the square bounds based on the damageRadius
        Vector2 pointA = new Vector2(transform.position.x - damageRadius / 2, transform.position.y - damageRadius / 2);
        Vector2 pointB = new Vector2(transform.position.x + damageRadius / 2, transform.position.y + damageRadius / 2);

        // Detect players within the square area
        Collider2D[] players = Physics2D.OverlapAreaAll(pointA, pointB, playerLayer);

        if (players.Length > 0)
        {
            //Debug.Log("Player detected in square area.");
            if (!isDamaging)
            {
                isDamaging = true;
                StartCoroutine(DamagePlayer(players[0]));  // Start damaging the player
            }
        }
        else
        {
            isDamaging = false;  // Stop damaging if no player is in range
        }
    }

    // Visualize the damage area in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        if (isCircular)
        {
            // Draw a wire sphere to show the circular damage radius
            Gizmos.DrawWireSphere(transform.position, damageRadius);
        }
        else
        {
            // Draw a wire cube to show the square damage area
            Gizmos.DrawWireCube(transform.position, new Vector3(damageRadius, damageRadius, 0));
        }
    }
}
