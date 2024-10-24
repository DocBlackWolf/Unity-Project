using System.Collections;
using UnityEngine;

public class SC_PunchTrap1 : MonoBehaviour
{
    // Configurable variables
    [SerializeField] private Vector2 pushDirection = new Vector2(-1, 0);  // Direction of the push
    [SerializeField] private float pushForce = 5f;                        // Force of the push
    [SerializeField] private float delayBeforePush = 0.5f;                // Delay before applying the push
    [SerializeField] private float cooldown = 2f;                         // Cooldown before the trigger can activate again

    public bool isCooldown = false;                    // Whether the trigger is on cooldown
    public bool playerInside = false;                  // Whether the player is inside the trigger

    public bool IsPlayerInside()
    {
        return playerInside;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCooldown)
        {
            playerInside = true;

            StartCoroutine(PushWithDelay(other));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    private IEnumerator PushWithDelay(Collider2D player)
    {
        isCooldown = true;

        yield return new WaitForSeconds(delayBeforePush);

        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();

        if (playerRb != null)
        {
            Vector2 force = pushDirection.normalized * pushForce;
            playerRb.AddForce(force, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(cooldown);

        isCooldown = false;
    }
}
