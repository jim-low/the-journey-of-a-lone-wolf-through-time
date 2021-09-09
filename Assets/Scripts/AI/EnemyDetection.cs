using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private Enemy enemy;

    private void Start()
    {
        enemy = transform.parent.GetComponent<Enemy>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) {
            transform.parent.GetComponent<Enemy>().hasDetectedPlayer = true;
            if (enemy.type.Equals("Knife"))
            {
                StartCoroutine(transform.parent.GetComponent<Melee>().Attack(collider.gameObject.GetComponent<Soldier>()));
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) {
            transform.parent.GetComponent<Enemy>().hasDetectedPlayer = false;
        }
    }
}
