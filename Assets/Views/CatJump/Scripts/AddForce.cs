using UnityEngine;

public class AddForce : MonoBehaviour
{
    public float jumpForce = 10f;
    public float jumpCooldown = 0.5f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float lastJumpTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time - lastJumpTime > jumpCooldown)
        {
            Jump();
            lastJumpTime = Time.time;
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            // Game over logic
            Debug.Log("Game Over");
        }
    }
}
