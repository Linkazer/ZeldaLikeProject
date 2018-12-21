using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementDecorScript : MonoBehaviour {

    Rigidbody2D rigiBoy;

    private void Start()
    {
        rigiBoy = GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && collision.gameObject != gameObject)
        {
            if (Input.GetButton("A"))
            {
                rigiBoy.constraints = RigidbodyConstraints2D.None;
                rigiBoy.constraints = RigidbodyConstraints2D.FreezeRotation;
                rigiBoy.MovePosition(rigiBoy.position + collision.gameObject.GetComponentInParent<DeplacementJoueurScript>().velocity * Time.fixedDeltaTime);
                collision.gameObject.GetComponentInParent<DeplacementJoueurScript>().isSlow = true;
                //transform.parent.gameObject.layer = LayerMask.NameToLayer("Player");
            }
            else
            {
                rigiBoy.constraints = RigidbodyConstraints2D.FreezeAll;
                //transform.parent.gameObject.layer = LayerMask.NameToLayer("Default");
                collision.gameObject.GetComponentInParent<DeplacementJoueurScript>().isSlow = false;
            }
        }
    }
}
