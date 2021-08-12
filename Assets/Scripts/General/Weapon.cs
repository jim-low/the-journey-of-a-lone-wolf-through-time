using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Camera cam;

    Rigidbody2D rb;
    Vector2 mousePosition;
    Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = gameObject.GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
    }

    void Aim()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePosition - rb.position;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
