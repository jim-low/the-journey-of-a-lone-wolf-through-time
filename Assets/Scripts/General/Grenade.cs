using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private float delay = 2.5f;
    private float radius = 20f;

    void Start()
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(delay);
        Vector3 explosionPoint = gameObject.transform.position;

        // play the animation
        // detect whoever is within radius and add damage
        // destroy game object
        Destroy(gameObject);
    }
}
