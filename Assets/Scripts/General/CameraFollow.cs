using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        Camera.main.orthographicSize = 7;
    }

    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
    }
}
