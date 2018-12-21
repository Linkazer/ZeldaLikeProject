using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(DeplacementGarde))]
public class FieldOfViewEditor : Editor {

    // Use this for initialization
    private void OnSceneGUI()
    {
        DeplacementGarde fow = (DeplacementGarde)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector2.right, 360, fow.distanceVue);
        Vector2 viewAngleA = fow.dirFromAngle(-360 / 2);
        Vector2 viewAngleB = fow.dirFromAngle(360 / 2);

        Handles.DrawLine(fow.transform.position, fow.transform.position + new Vector3(viewAngleA.x, viewAngleA.y, 0) * fow.distanceVue);
        Handles.DrawLine(fow.transform.position, fow.transform.position + new Vector3(viewAngleB.x, viewAngleB.y, 0) * fow.distanceVue);

        Handles.color = Color.red;
        foreach(Transform visibleTarget in fow.visibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
        }

    }
}
