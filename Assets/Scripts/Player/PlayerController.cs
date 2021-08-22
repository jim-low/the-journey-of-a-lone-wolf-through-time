using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float jumpForce = 5f;
    public float slidingSpeed = 10f;
    public float rollDist = 7f;

    private Vector3 rollDestination;
    private SpriteRenderer sr;
    private Rigidbody2D rigidBody;

    bool onGround = true;
    bool rolling = false;
    float horizontalDir;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        FaceDirection();

        if (!rolling) {
            Move();
        }

        if (onGround && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))) {
            Jump();
        }

        if (horizontalDir != 0 && Input.GetKeyDown(KeyCode.LeftControl)) {
            Slide();
        }

        if (!rolling && onGround && Input.GetKeyDown(KeyCode.LeftShift)) {
            rolling = true;
            rollDestination = transform.position;
            rollDestination.x += rollDist;
            StartCoroutine(StopRoll(0.5f));
        }

        if (rolling) {
            Roll();
        }
    }

    void Move()
    {
        horizontalDir = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(horizontalDir, 0, 0) * moveSpeed * Time.deltaTime;
    }

    void Jump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        onGround = false;
    }

    void FaceDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        sr.flipX = Mathf.Abs(angle) > 90;
    }

    IEnumerator StopRoll(float stopTime) {
        yield return new WaitForSeconds(stopTime);
        rolling = false;
    }

    void Roll()
    {
        const float rollScalar = 2f;
        float rollSpeed = moveSpeed * rollScalar * Time.deltaTime;
        /* if (sr.flipX) { // if looking left, roll left */
        /*     rollSpeed = Mathf.Abs(rollSpeed) * -1; */
        /* } */

        transform.position = Vector3.MoveTowards(transform.position, rollDestination, rollSpeed);
    }

    void Slide()
    {
        rigidBody.AddForce(Vector2.right * slidingSpeed * horizontalDir, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collided)
    {
        onGround = true;
    }
}
