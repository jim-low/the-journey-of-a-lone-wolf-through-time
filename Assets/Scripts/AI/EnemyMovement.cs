using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float patrolDistance = 1f;
    public float MOVE_SPEED;
    public float moveSpeed;
    public Animator animator;

    [Range(1, 3)]
    public float pauseSeconds = 1f;

    private Vector3 startPos;
    private Vector3 endPos;
    private SpriteRenderer sr;


    bool pausing = false;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + new Vector3(patrolDistance, 0f, 0f);
        sr = GetComponent<SpriteRenderer>();
        moveSpeed = Soldier.moveSpeed;
        MOVE_SPEED = Soldier.moveSpeed;
    }

    void Update()
    {
        if (!pausing) {
            Move();
        }
    }

    void Move()
    {
        animator.SetBool("isMoving", true);
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        if (transform.position.x <= startPos.x || transform.position.x >= endPos.x) {
            pausing = true;

            StartCoroutine(ChangeDirection());
        }
    }

    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(pauseSeconds);
        animator.SetBool("isMoving", false);
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        //sr.flipX = !sr.flipX;
        moveSpeed = currentScale.x < 0 ? -MOVE_SPEED : MOVE_SPEED;
        pausing = false;
    }
}
