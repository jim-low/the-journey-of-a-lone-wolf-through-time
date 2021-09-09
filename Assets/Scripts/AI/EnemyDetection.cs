using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) {
            transform.parent.GetComponent<Enemy>().hasDetectedPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) {
            transform.parent.GetComponent<Enemy>().hasDetectedPlayer = false;
        }
    }
}
