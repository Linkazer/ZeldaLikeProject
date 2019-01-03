using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpideDetaction : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Obstacle")
        {
            GetComponentInParent<ScorpideComportement>().WakeUp(collision.transform);
        }
    }
}
