using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SC_Attack : MonoBehaviour
{
    public float attackRange = 1f;  // Range of the attack
    public Transform attackPoint;   // Point from where the attack is initiated
    public LayerMask hitboxLayer;   // Layer for detecting hitboxes (tilemap colliders)
    public float attackRate = 1f;   // How often the player can attack
    public float pushForce = 5f;    // The force applied to the player when attacking near a hitbox
    public float upwardPushForce = 1f;  // Variable to control upward push force
    private float nextAttackTime = 0f;

    public AudioSource attackAudioSource;  // Reference to the AudioSource component
    public AudioClip attackSound;

    public Animator animator;  // Reference to the Animator component
    private bool isAttacking = false; // Flag to check if attack animation is already playing

    private Rigidbody2D playerRb; // Reference to the player's Rigidbody2D component

    void OnEnable()
    {
        playerRb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component for the player
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isAttacking)
            {
                StartAttack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

void StartAttack()
{
    // Play attack sound
    if (attackAudioSource != null && attackSound != null)
    {
        attackAudioSource.PlayOneShot(attackSound);
    }

    // Play attack animation
    if (animator != null)
    {
        animator.SetBool("Attacking", true);
        isAttacking = true;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;

        StartCoroutine(ResetAttackBool(animationLength));
    }

    // Check if the player collides with any tilemap hitbox during the attack
    Collider2D[] hitboxes = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, hitboxLayer);

    foreach (Collider2D hitbox in hitboxes)
    {
        // Check if the hitbox has a TilemapCollider2D or CompositeCollider2D
        if (hitbox is TilemapCollider2D || hitbox is CompositeCollider2D)
        {
            Debug.Log("Tilemap hitbox detected, applying push to the player.");

            // Set downward (y) velocity to 0 to stop falling
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0f);

            // Directly apply upward force separately (without normalizing)
            Vector2 upwardPush = new Vector2(0f, upwardPushForce);

            // Also apply the horizontal push separately
            Vector2 horizontalPush = new Vector2(-playerRb.velocity.x, 0f);

            // Apply both the horizontal and upward forces
            playerRb.AddForce(horizontalPush * pushForce, ForceMode2D.Impulse);
            playerRb.AddForce(upwardPush, ForceMode2D.Impulse); // Strong upward push
        }
    }
}


    IEnumerator ResetAttackBool(float animationLength)
    {
        yield return new WaitForSeconds(animationLength);

        if (animator != null)
        {
            animator.SetBool("Attacking", false);
            isAttacking = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
