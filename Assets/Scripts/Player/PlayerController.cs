using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float jumpForce = 5f;
    public float slidingSpeed = 10f;
    public float rollDist = 7f;
    public float moveSpeed = 7f;
    [SerializeField] private AudioSource roll;
    [SerializeField] private AudioSource slide;

    private Vector3 rollDestination;
    private SpriteRenderer sr;
    private Rigidbody2D rigidBody;
    private Soldier soldier;


    bool onGround = true;
    bool rolling = false;
    bool prone = true;
    bool crouch = true;
    float horizontalDir;

    void Start()
    {
        soldier = GetComponent<Soldier>();
        rigidBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (GameController.paused) {
            return;
        }

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
            slide.Play();
            Slide();
        }

        if (!rolling && onGround && Input.GetKeyDown(KeyCode.LeftShift)) {
            rolling = true;
            rollDestination = transform.position;
            rollDist = sr.flipX ? -Mathf.Abs(rollDist) : Mathf.Abs(rollDist);
            rollDestination.x += rollDist;
            StartCoroutine(StopRoll(0.5f));
        }

        if (rolling) {
            animator.SetBool("isRolling", true);
            roll.Play();
            Roll();
        }

        if (Input.GetKeyDown(KeyCode.G)) {
            soldier.Die();
        }

        if (onGround && Input.GetKeyDown(KeyCode.C)) {
            Prone();
        }

        if (onGround && horizontalDir == 0 && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftControl))) {
            Crouch();
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

    IEnumerator StopSlide(float stopTime)
    {
        yield return new WaitForSeconds(stopTime);
        animator.SetBool("isSlide", false);
    }


    void Roll()
    {
        const float rollScalar = 2f;
        float rollSpeed = moveSpeed * rollScalar * Time.deltaTime;

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
