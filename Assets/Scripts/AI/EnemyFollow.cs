using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float followRange = 20f;
    /* public Animator animator; */

    private EnemyMovement enemyMovement;
    private Enemy enemy;
    private Vector3 playerPos;

    bool following = false;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyMovement = GetComponent<EnemyMovement>();
        playerPos = GameObject.Find("Player").transform.position;
    }

    void Update()
    {
        if (!following) {
            DeterminePlayerDistance();
        }

        playerPos = GameObject.Find("Player").transform.position;
        Vector3 scale = transform.localScale;
        if (following && Mathf.Abs(transform.position.x - playerPos.x) >= followRange) {
            /* animator.SetBool("isMoving", true); */
            if (transform.position.x < playerPos.x) {
                transform.Translate(new Vector3(Soldier.moveSpeed, 0, 0) * Time.deltaTime);
                scale.x = Mathf.Abs(scale.x);
            }
            else {
                transform.Translate(new Vector3(-Soldier.moveSpeed, 0, 0) * Time.deltaTime);
                scale.x = -Mathf.Abs(scale.x);
            }

        }
        else
        {
            /* animator.SetBool("isMoving", false); */
        }
        transform.localScale = scale;
    }

    void DeterminePlayerDistance()
    {
        float distance = Vector3.Distance(transform.position, playerPos);

        if (distance < followRange) {
            following = true;
            enemyMovement.enabled = false;
            if (enemy.type.Equals("Knife"))
            {
                followRange = 2f;
            }
        }
    }
}
