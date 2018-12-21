using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeRondePoint : MonoBehaviour {

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Color color = Color.blue;
        color.a = 0.42f;
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
