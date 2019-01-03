using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

    public float maxHealth;
    private float currentHealth;
    public bool isEnemy = true;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
	}
	
	public void TakeDamage(float dmgTaken)
    {
        currentHealth -= dmgTaken;
        if (currentHealth <= 0)
        {
            IsDying();
        }
    }

    private void IsDying()
    {
        Destroy(transform.parent.gameObject);
    }
}
