using System.Collections;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [Header("Damage Configuration")]
    public float damageAmount = 1f;   
    public float damageInterval = 1f;   
    public LayerMask playerLayer;       
    private bool isDamaging = false;    
    private Collider2D playerInRange;   

    private IEnumerator DamagePlayer()
    {
        while (isDamaging)
        {
            if (playerInRange != null)
            {
                SC_Life playerLife = playerInRange.GetComponent<SC_Life>();
                if (playerLife != null)
                {
                    playerLife.ModificarVida(-damageAmount); 
                    Debug.Log("Player damaged: " + damageAmount);
                }
                else
                {
                    Debug.LogWarning("No SC_Life component found on player!");
                }
            }

            // Wait for the specified damage interval before damaging again
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
    }
}
