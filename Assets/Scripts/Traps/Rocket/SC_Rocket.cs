using UnityEngine;

public class SC_Rocket : MonoBehaviour
{
    [SerializeField] public Vector2 movementDirection = Vector2.right;

    [SerializeField] public float speed = 5f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        movementDirection = movementDirection.normalized;
    }

    private void Update()
    {
        rb.velocity = movementDirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
