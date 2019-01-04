using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureDeplacement : MonoBehaviour {

    public float speed;

    Vector2 velocity;
    Rigidbody2D rigiBoy;

    Vector2 startPosition;
    private float walkDistance = 0.2f;
    private float maxDistance = 4;

    private Vector2 target;
    private bool canMove = true;

	// Use this for initialization
	void Start () {
        rigiBoy = GetComponent<Rigidbody2D>();
        target = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (canMove)
        {
            velocity = (target - new Vector2(transform.position.x, transform.position.y)).normalized * speed;
        }

        if (Vector2.Distance(target, transform.position) <= walkDistance && canMove)
        {
            StartCoroutine(stayHere());
        }
	}

    private void FixedUpdate()
    {
        if (canMove)
        {
            rigiBoy.MovePosition(rigiBoy.position + velocity * Time.fixedDeltaTime);
        }
    }

    IEnumerator stayHere()
    {
        canMove = false;
        target = transform.position;
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        canMove = true;
        target = startPosition + new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopAllCoroutines();
        StartCoroutine(stayHere());
    }
}
