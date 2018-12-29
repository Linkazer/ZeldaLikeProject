using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSDebug : MonoBehaviour {

    private float deltaTime;

	// Update is called once per frame
	void Update () {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1 / deltaTime;
        Debug.Log(fps);
	}
}
