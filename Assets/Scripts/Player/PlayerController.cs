using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 1f;
    public float jumpForce = 5f;
    public float slidingSpeed = 10f;
    public float rollDist = 7f;
    public GameObject grenadePrefab;

    private Vector3 rollDestination;
    private SpriteRenderer sr;
    private Rigidbody2D rigidBody;
    private GameObject grenadeLaunchPoint;

    bool onGround = true;
    bool rolling = false;
    bool prone = true;
    float horizontalDir;
    float grenadeThrowForce = 15f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        grenadeLaunchPoint = GameObject.Find("GrenadeLaunchPoint");
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

        if (!rolling && onGround && Input.GetKeyDown(KeyCode.LeftShift)) {

            rolling = true;

            rollDestination = transform.position;
            rollDist = sr.flipX ? -Mathf.Abs(rollDist) : Mathf.Abs(rollDist);
            rollDestination.x += (rollDist);
            StartCoroutine(StopRoll(0.5f));
        }

        /* if (Input.GetKeyDown(KeyCode.G)) { */
        /*     ThrowGrenade(); */
        /* } */

        if (rolling) {
            animator.SetBool("isRolling", true);
            Roll();
        }

        if(onGround && Input.GetKeyDown(KeyCode.C))
        {
            Prone();
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

        Soldier soldier = gameObject.GetComponent<Soldier>();
        float damage = Weapon.currentWeapon.GetComponent<Gun>().damage;
        Soldier.Damage(soldier, damage);
    }

    void FaceDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        sr.flipX = Mathf.Abs(angle) > 90;
    }

    /* void ThrowGrenade() */
    /* { */
    /*     // calculate throw angle */
    /*     Vector3 grenadeLaunchPos = Camera.main.ScreenToWorldPoint(grenadeLaunchPoint.transform.position); */
    /*     Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); */
    /*     float grenadeThrowAngle = Mathf.Atan2(mousePos.y - grenadeLaunchPos.y, mousePos.x - grenadeLaunchPos.x) * Mathf.Rad2Deg; */
    /*     Debug.Log(grenadeThrowAngle); */

    /*     // set grenade start position */
    /*     GameObject grenade = Instantiate(grenadePrefab); */
    /*     grenade.transform.position = gameObject.transform.position; */

    /*     // set grenade velocity */
    /*     Vector2 grenadeVelocity = Vector2.one * grenadeThrowForce; */
    /*     // TODO: correctly calculate throw angle */
    /*     grenadeVelocity.x += Mathf.Cos(grenadeThrowAngle); */
    /*     grenadeVelocity.y += Mathf.Sin(grenadeThrowAngle); */
    /*     grenade.GetComponent<Rigidbody2D>().velocity = grenadeVelocity; */
    /*     Debug.Log(grenadeVelocity); */
    /* } */

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


    void OnCollisionEnter2D(Collision2D collided)
    {
        animator.SetBool("isJumping", false);
        onGround = true;
    }
}
