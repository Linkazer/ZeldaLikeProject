using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilDeplacement : MonoBehaviour {

    public float maxSpeed = 12;
    public float portee = 50000000;

    public float speed = 12;

    public float currentPortee;

    public bool isSound;
    public bool isLight;

    public Vector2 velocity;
    private Vector2 startPosition;

    public bool makeDiversion;

    Rigidbody2D rigiBoy;

    private FieldOfViewProjectile fov;

	// Use this for initialization
	void Awake () {
        startPosition = transform.position;
        rigiBoy = GetComponent<Rigidbody2D>();
        fov = GetComponent<FieldOfViewProjectile>();
    }
	
	// Update is called once per frame
	void Update () {
		if(Vector2.Distance(transform.position,startPosition)>=currentPortee)
        {
            EstDivertissant();
        }
	}

    private void FixedUpdate()
    {
        rigiBoy.MovePosition(rigiBoy.position + velocity * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Obstacles" || collision.gameObject.tag == "Ennemis")
        {
            EstDivertissant();
        }
    }

    void EstDivertissant()
    {
        if (fov.visiblePlayer.Count > 0)
        {
            foreach (DeplacementDesGardesV2 target in fov.visiblePlayer)
            {
                target.eardSomething = true;
                target.lastKnownPosition = transform.position;
            }
        }
        Destroy(gameObject, 0.2f);
        velocity = Vector2.zero;
    }
}
