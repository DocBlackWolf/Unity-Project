using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] protected float speed = 5f;
    [SerializeField] private int latchTime = 3;
    [SerializeField] private int maxLatches = 3;
    [SerializeField] private float speedReductionFactor = 0.3f;

    public Vector2 Direction;

    private Rigidbody2D myRigidbody2D;
    private SC_Life life_Script;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private CircleCollider2D bodyCollider;
    [SerializeField] private Collider2D feetCollider;

    public bool CanJump { get; private set; } = true;
    public bool Jumping { get; protected set; } = false;
    public bool Latching { get; protected set; } = false;
    public bool CanLatch { get; private set; } = false;

    private int currentLatches;

    private void OnEnable()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        life_Script = GetComponent<SC_Life>();
        currentLatches = maxLatches;
    }

    private void Update()
    {
        Direction = Vector2.zero;

        if (!Latching && life_Script.EstasVivo())
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                Direction.x = 1f;
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                Direction.x = -1f;
        }

        CanJump = IsGrounded();

        if (Input.GetKeyDown(KeyCode.Space) && CanJump && life_Script.EstasVivo())
            Jumping = true;

        if (Input.GetKeyDown(KeyCode.E) && CanLatch && !CanJump && currentLatches > 0)
        {
            Latching = true;
            currentLatches--;
            StartCoroutine(Latch());
        }

        if (Input.GetKeyDown(KeyCode.Space) && Latching)
        {
            Jumping = true;
            Latching = false;
            StopAllCoroutines();
            myRigidbody2D.gravityScale = 1f;
        }
    }

    private void FixedUpdate()
    {
        if (Direction.x != 0 && !Latching)
            myRigidbody2D.AddForce(Direction * speed);

        if (IsGrounded() && Direction.x == 0)
        {
            
            Vector2 reducedDirection = myRigidbody2D.velocity * speedReductionFactor;

            myRigidbody2D.velocity = reducedDirection;
        }


        if (Jumping)
        {
            myRigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Jumping = false;
        }

        if (Latching)
            myRigidbody2D.velocity = Vector2.zero;

        if (IsGrounded())
        {
            Jumping = false;
            Latching = false;
            currentLatches = maxLatches;
        }

        if (!IsGrounded() && !Latching)
        {
            // Jump logic
        }

        CanLatch = IsWalled();
    }

    public bool IsGrounded()
    {
        return feetCollider.IsTouchingLayers(groundLayer);
    }

    private bool IsWalled()
    {
        return bodyCollider.IsTouchingLayers(groundLayer);
    }

    private IEnumerator Latch()
    {
        myRigidbody2D.gravityScale = 0f;
        myRigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(latchTime);
        myRigidbody2D.gravityScale = 1f;
        Latching = false;
        CanLatch = false;
    }
}
