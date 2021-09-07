using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medic : MonoBehaviour
{
    [SerializeField]
    private float healAmount = 15;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy")) {
            collider.GetComponent<Soldier>().setHeal(true);
            StartCoroutine(collider.GetComponent<Soldier>().Heal(healAmount));
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy")) {
            collider.GetComponent<Soldier>().setHeal(false);
        }
    }
}
