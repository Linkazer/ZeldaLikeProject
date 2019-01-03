using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpideVision : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Player" || collision.tag == "Ennemis" || collision.tag == "Creature")
        {

        }
    }
}
