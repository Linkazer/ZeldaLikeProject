using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamageScript : MonoBehaviour {

    public bool isEnemy = true;
    public float dmg;
    public bool doesHitWall = false;

    // Use this for initialization

    private void OnEnable()
    {
        StartCoroutine(TimeAttack());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Health")
        {
            if(collision.gameObject.GetComponent<HealthScript>().isEnemy != isEnemy || gameObject.tag == "CreatureWeapon")
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
