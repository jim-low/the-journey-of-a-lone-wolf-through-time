using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed = 1f;
    public float jumpForce = 5f;
    public float slidingSpeed = 20f;

    private SpriteRenderer sr;
    private Rigidbody2D rigidBody;

    bool onGround = true;
    float horizontalDir;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        Jump();
        Slide();
    }

    void Move()
    {
        horizontalDir = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(horizontalDir, 0, 0) * moveSpeed * Time.deltaTime;
    }

    void Jump()
    {
        if (!onGround)
            return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            onGround = false;
            Debug.Log("jumpinggggggggggg");
        }
    }

    void Slide()
    {
        if (horizontalDir == 0)
            return;

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            rigidBody.AddForce(Vector2.right * slidingSpeed * horizontalDir * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collided)
    {
        onGround = collided.collider.CompareTag("Ground");
    }
}
