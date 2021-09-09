using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float followRange = 20f;

    private EnemyMovement enemyMovement;
    private Vector3 playerPos;

    bool following = false;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        playerPos = GameObject.Find("Player").transform.position;
    }

    void Update()
    {
        if (!following) {
            DeterminePlayerDistance();
        }

        playerPos = GameObject.Find("Player").transform.position;

        if (following && Mathf.Abs(transform.position.x - playerPos.x) >= followRange) {
            if (transform.position.x < playerPos.x) {
                transform.Translate(new Vector3(Soldier.moveSpeed, 0, 0) * Time.deltaTime);
            }
            else {
                transform.Translate(new Vector3(-Soldier.moveSpeed, 0, 0) * Time.deltaTime);
            }
        }
    }

    void DeterminePlayerDistance()
    {
        float distance = Vector3.Distance(transform.position, playerPos);
        Debug.Log(distance);

        if (distance < followRange) {
            following = true;
            enemyMovement.enabled = false;
        }
    }
}
