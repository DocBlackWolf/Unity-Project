using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Attack : MonoBehaviour
{
    public float attackRange = 1f;  // Range of the attack
    public Transform attackPoint;   // Point from where the attack is initiated
    public LayerMask pushableLayers;   // Layers considered for pushing
    public float attackRate = 1f;   // How often the player can attack
    public float pushForce = 5f;    // The force applied to objects when pushed
    private float nextAttackTime = 0f;
    public AudioSource attackAudioSource;  // Reference to the AudioSource component
    public AudioClip attackSound;

    public Animator animator;  // Reference to the Animator component
    private bool isAttacking = false; // Flag to check if attack animation is already playing

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

        if (attackAudioSource != null && attackSound != null)
        {
            attackAudioSource.PlayOneShot(attackSound);
        }

 
        if (animator != null)
        {
            animator.SetBool("Attacking", true);
            isAttacking = true;


            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            float animationLength = stateInfo.length;

            StartCoroutine(ResetAttackBool(animationLength));
        }


        Collider2D[] objectsToPush = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, pushableLayers);

        if (objectsToPush.Length == 0)
        {
            Debug.Log("No objects in range to push.");
        }

        foreach (Collider2D obj in objectsToPush)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Debug.Log("Found object with Rigidbody2D: " + obj.name);

                Vector2 pushDirection = (obj.transform.position - attackPoint.position).normalized;

                Debug.Log("Pushing " + obj.name + " with force " + (pushDirection * pushForce));

                rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }
            else
            {
                Debug.Log(obj.name + " does not have a Rigidbody2D.");
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
