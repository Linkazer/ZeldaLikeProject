using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamageScript : MonoBehaviour {

    public int ownType;
    public float dmg;
    public bool doesHitWall = false;
    public Cristal cristalType;

    // Use this for initialization

    private void OnEnable()
    {
        if (gameObject.tag != "Projectile")
        {
            StartCoroutine(TimeAttack());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Health")
        {
            if(collision.gameObject.GetComponent<HealthScript>().ownType != ownType || ownType == 3)
            {
                collision.gameObject.GetComponent<HealthScript>().TakeDamage(dmg);
            }
        }
    }

    IEnumerator TimeAttack()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }
}
