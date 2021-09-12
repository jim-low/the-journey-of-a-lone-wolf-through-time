using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    /* public Animator animator; */

    private float followRange = 20f;
    private Enemy enemy;
    private Vector3 playerPos;

    bool following = false;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        playerPos = GameObject.Find("Player").transform.position;
        if (!enemy.type.Equals("Knife")) {
            followRange = GetComponentInChildren<CircleCollider2D>().radius;
        }
    }

    void Update()
    {
        playerPos = GameObject.Find("Player").transform.position;
        if (!following) {
            DeterminePlayerDistance();
        }
        else {
            FacePlayer();
            MoveEnemy();
        }
    }

    void MoveEnemy()
    {
        int direction = (transform.position.x - playerPos.x) < 0 ? 1 : -1; // -1 = left, 1 = right

        if (Mathf.Abs(transform.position.x - playerPos.x) >= followRange) {
            transform.Translate(new Vector3(Soldier.moveSpeed * direction, 0, 0) * Time.deltaTime);
        }
    }

    void FacePlayer()
    {
        Vector3 scale = transform.localScale;
        if (transform.position.x < playerPos.x) {
            scale.x = Mathf.Abs(scale.x);
        }
        else {
            scale.x = -Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }

    void DeterminePlayerDistance()
    {
        float distance = Vector3.Distance(transform.position, playerPos);

        if (distance <= followRange) {
            following = true;
            gameObject.GetComponent<EnemyMovement>().enabled = false;
            if (enemy.type.Equals("Knife"))
            {
                followRange = 2f;
            }
        }
    }
}
