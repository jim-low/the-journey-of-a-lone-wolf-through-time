using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float patrolDistance = 1f;
    public float MOVE_SPEED;
    public float moveSpeed;

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
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        if (transform.position.x < startPos.x || transform.position.x > endPos.x) {
            StartCoroutine(ChangeDirection());
        }
    }

    IEnumerator ChangeDirection()
    {
        pausing = true;
        moveSpeed = 0;
        yield return new WaitForSeconds(pauseSeconds);
        sr.flipX = !sr.flipX;
        moveSpeed = sr.flipX ? -MOVE_SPEED : MOVE_SPEED;
        pausing = false;
    }
}
