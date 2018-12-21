using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaringTrigger : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<DeplacementJoueurScript>().makeNoise)
            {
                GetComponentInParent<DeplacementDesGardesV2>().eardSomething = true;
                GetComponentInParent<DeplacementDesGardesV2>().lastKnownPosition = collision.transform.position;
            }
        }
    }

}
