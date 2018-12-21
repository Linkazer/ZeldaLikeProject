using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlockScript : MonoBehaviour {

    CameraDeplacementScript camScript;

    // Use this for initialization
    private void OnTriggerStay2D(Collider2D collision)
    {
       // Debug.Log("test");
        if (collision.gameObject.tag == "MainCamera")
        {
            //Debug.Log("test");
            camScript = collision.gameObject.GetComponent<CameraDeplacementScript>();
        }
    }
}
