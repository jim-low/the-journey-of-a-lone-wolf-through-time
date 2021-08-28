using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 2.5f, -10f);

    [Range(1, 10)]
    public float smoothFactor = 3.65f;

    public Transform target;

    void Start()
    {
        Camera.main.orthographicSize = 10;
    }

    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smoothTransition = Vector3.Lerp(transform.position, targetPos, smoothFactor * Time.deltaTime);

        transform.position = smoothTransition;
    }

    void OnCollisionTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.tag);
    }
}
