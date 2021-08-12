using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed = 1f;
    public float jumpForce = 5f;

    private SpriteRenderer sr;
    private Rigidbody2D rigidBody;

    bool onGround = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        Jump();
        FaceDirection();
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(horizontal, 0, 0) * moveSpeed * Time.deltaTime;
    }

    void Jump()
    {
        if (!onGround)
            return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            onGround = false;
        }
    }

    void FaceDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 playerToMouse = mousePos - playerPos;

        Vector3 playerScale = transform.localScale;
        GameObject weapon = GameObject.Find("Weapon");
        Vector3 weaponScale = weapon.transform.localScale;

        if (playerToMouse.x < 0) {
            playerScale.x = -Mathf.Abs(playerScale.x);
            weaponScale.x = -Mathf.Abs(weaponScale.x);
            weaponScale.y = -Mathf.Abs(weaponScale.y);
        }
        else if (playerToMouse.x > 0) {
            playerScale.x = Mathf.Abs(playerScale.x);
            weaponScale.x = Mathf.Abs(weaponScale.x);
            weaponScale.y = Mathf.Abs(weaponScale.y);
        }

        transform.localScale = playerScale;
        weapon.transform.localScale = weaponScale;
    }

    void OnCollisionEnter2D(Collision2D collided)
    {
        onGround = collided.collider.CompareTag("Ground");
    }
}
