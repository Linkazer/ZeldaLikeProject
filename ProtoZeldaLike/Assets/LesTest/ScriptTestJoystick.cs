using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTestJoystick : MonoBehaviour {


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("Enter Space");
           // Debug.Log("Axe X = " + Input.GetAxisRaw("Horizontal"));
            //Debug.Log("Axe Y = " + Input.GetAxisRaw("Vertical"));
            transform.position = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }

}
