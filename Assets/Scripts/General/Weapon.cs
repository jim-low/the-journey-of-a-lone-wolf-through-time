using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Camera cam;
    public Rigidbody2D rb;
    Vector2 mousePosition;

    // Update is called once per frame
    void Update()
    {
        Aim();
    }

    void Aim()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePosition - rb.position;
        // TODO:
        // fix player rotation sync with weapon rotation
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
