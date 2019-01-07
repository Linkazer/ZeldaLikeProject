using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

    public float maxHealth;
    private float currentHealth;
    public int ownType;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
	}
	
	public void TakeDamage(float dmgTaken)
    {
        if (ownType != 4)
        {
            currentHealth -= dmgTaken;
            if (currentHealth <= 0)
            {
                IsDying();
            }
        }
        else
        {

        }
    }

    private void IsDying()
    {
        Destroy(transform.parent.gameObject);
    }
}
