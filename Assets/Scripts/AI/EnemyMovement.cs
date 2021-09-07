using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float patrolDistance = 1f;
    public const float MOVE_SPEED = 5f;
    public float moveSpeed = 5f;

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
