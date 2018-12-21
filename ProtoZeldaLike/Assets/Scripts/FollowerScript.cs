using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerScript : MonoBehaviour {

    public Transform target;
    public float smoothSpeed = 0.125f;
    private Vector3 smoothedPosition;

    public Vector2 margin = new Vector2(1, 1);

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position;
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        if(Mathf.Abs(transform.position.x-target.position.x)>margin.x || Mathf.Abs(transform.position.y - target.position.y) > margin.y)
            transform.position = smoothedPosition;
    }
}
