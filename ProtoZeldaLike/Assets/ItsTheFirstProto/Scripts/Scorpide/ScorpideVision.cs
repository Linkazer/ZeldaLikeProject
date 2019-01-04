using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpideVision : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Player" || collision.tag == "Ennemis" || collision.tag == "Creature")
        {
            if(Physics2D.Raycast(transform.position, collision.transform.position-transform.position, Vector2.Distance(transform.position,collision.transform.position)).collider.tag != "Obstacle")
            {
                GetComponentInParent<ScorpideComportement>().canReachTarget.Add(collision.transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GetComponentInParent<ScorpideComportement>().canReachTarget.Contains(collision.transform))
        {
            GetComponentInParent<ScorpideComportement>().canReachTarget.Remove(collision.transform);
        }

    }
}
