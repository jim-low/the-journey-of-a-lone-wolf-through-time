using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstantDeath : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) {
            SceneManager.LoadScene("LoseScreen");
        }
        else if (collider.CompareTag("Enemy")) {
            Destroy(collider.gameObject);
        }
    }
}
