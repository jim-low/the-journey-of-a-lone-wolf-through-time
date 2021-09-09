using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float jumpForce = 5f;
    public float slidingSpeed = 10f;
    public float rollDist = 7f;
    public GameObject grenadePrefab;

    private Vector3 rollDestination;
    private SpriteRenderer sr;
    private Rigidbody2D rigidBody;
    /* private GameObject grenadeLaunchPoint; */
    private Soldier soldier;

    bool onGround = true;
    bool rolling = false;
    bool prone = true;
    bool crouch = true;
    float horizontalDir;
    float grenadeThrowForce = 15f;

    void Start()
    {
        soldier = GetComponent<Soldier>();
        rigidBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        /* grenadeLaunchPoint = GameObject.Find("GrenadeLaunchPoint"); */
    }

    void Update()
    {
        animator.SetFloat("Horizontal", Mathf.Abs(horizontalDir));

        FaceDirection();

        if (!rolling) {
            animator.SetBool("isRolling", false);
            Move();
        }

        if (onGround && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))) {
            Jump();
            animator.SetBool("isJumping", true);
        }

        if (horizontalDir != 0 && Input.GetKeyDown(KeyCode.LeftControl)) {
            animator.SetBool("isSlide", true);
            Slide();
        }
        /*
        if (!rolling && onGround && Input.GetKeyDown(KeyCode.LeftShift)) {
            rolling = true;

            rollDestination = transform.position;
            rollDist = sr.flipX ? -Mathf.Abs(rollDist) : Mathf.Abs(rollDist);
            rollDestination.x += (rollDist);
            StartCoroutine(StopRoll(0.5f));
        }*/

        if (rolling) {
            animator.SetBool("isRolling", true);
            Roll();
        }

        if(onGround && Input.GetKeyDown(KeyCode.C))
        {
            Prone();
        }

        if (onGround && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftControl)))
        {
            Crouch();
        }
    }

    void Move()
    {
        horizontalDir = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(horizontalDir, 0, 0) * Soldier.moveSpeed * Time.deltaTime;
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
        Vector3 currentScale = transform.localScale;
        currentScale.x = Mathf.Abs(angle) > 90 ? -Mathf.Abs(currentScale.x) : Mathf.Abs(currentScale.x);
        transform.localScale = currentScale;
    }

    IEnumerator StopRoll(float stopTime) {
        yield return new WaitForSeconds(stopTime);
        rolling = false;
    }
    IEnumerator StopSlide(float stopTime)
    {
        yield return new WaitForSeconds(stopTime);
        animator.SetBool("isSlide", false);
    }


    void Roll()
    {
        const float rollScalar = 2f;
        float rollSpeed = Soldier.moveSpeed * rollScalar * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, rollDestination, rollSpeed);
    }

    void Slide()
    {
        rigidBody.AddForce(Vector2.right * slidingSpeed * horizontalDir, ForceMode2D.Impulse);
        StartCoroutine(StopSlide(1.2f));
    }

    void Prone()
    {

        if (prone)
        {
            animator.SetBool("isProne", true);
            prone = false;
        }
        else
        {
            animator.SetBool("isProne", false);
            prone = true;
        }

    }
    void Crouch()
    {

        if (crouch)
        {
            animator.SetBool("isCrouch", true);
            crouch = false;
        }
        else
        {
            animator.SetBool("isCrouch", false);
            crouch = true;
        }

    }



    void OnCollisionEnter2D(Collision2D collided)
    {
        animator.SetBool("isJumping", false);
        onGround = true;
    }
}
