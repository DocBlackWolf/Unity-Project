using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Jump : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private float fuerzaSalto = 5f;
    [SerializeField] private LayerMask groundLayer;    // Layer for ground
    [SerializeField] private Collider2D feetCollider;  // Collider at the feet

    private bool puedoSaltar = true;   // Can jump
    private bool saltando = false;     // Is jumping
    private Rigidbody2D miRigidbody2D; // Reference to Rigidbody2D

    private void OnEnable()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();   // Get Rigidbody2D component
    }

    void Update()
    {
        // Check if player is on the ground
        puedoSaltar = IsGrounded();

        // Check for jump input and if grounded
        if (Input.GetKeyDown(KeyCode.Space) && puedoSaltar)
        {
            saltando = true;
        }
    }

    private void FixedUpdate()
    {
        if (saltando)
        {
            // Perform the jump
            miRigidbody2D.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            saltando = false; // Set jumping flag to false
        }
    }

    // Check if the feetCollider is touching the ground layer
    private bool IsGrounded()
    {
        return feetCollider.IsTouchingLayers(groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If touching ground after a jump, reset jumping state
        if (IsGrounded())
        {
            saltando = false;
        }
    }
}
