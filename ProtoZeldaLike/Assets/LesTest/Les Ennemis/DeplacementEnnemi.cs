using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementEnnemi : MonoBehaviour {

    public Transform targetPlayer;

    private bool isVisible;

    public float viewDistance;
    public float speed;
    public float walkDistance;

    private Vector2 lastPosition;
    private Vector2 playerPosition;
    private Vector2 currentPosition;

    private Rigidbody2D rigiBoy;
    private Vector2 velocity;

    LayerMask layerMask;

	// Use this for initialization
	void Start () {
        rigiBoy = GetComponent<Rigidbody2D>();
        layerMask = ~(1 << LayerMask.NameToLayer("Vision") | 1 << LayerMask.NameToLayer("Ennemis"));
        currentPosition = new Vector2(transform.position.x, transform.position.y);
        lastPosition = currentPosition;
    }
	
	// Update is called once per frame
	void Update () {
        playerPosition = new Vector2(targetPlayer.position.x, targetPlayer.position.y);
        currentPosition = new Vector2(transform.position.x, transform.position.y);

        RaycastHit2D hit = Physics2D.Raycast(currentPosition, (playerPosition - currentPosition).normalized,Mathf.Infinity,layerMask);

        if (hit.collider.gameObject.tag == "Player" && Vector2.Distance(currentPosition, playerPosition) <= viewDistance)
        {
            lastPosition = playerPosition;
        }
        
        if (Vector2.Distance(lastPosition, currentPosition) > walkDistance)
        {
            velocity = new Vector2(lastPosition.x - currentPosition.x, lastPosition.y - currentPosition.y).normalized * speed ;
        }
        else
        {
            velocity = Vector2.zero;
        }

        //Debug.DrawRay(currentPosition + ((playerPosition - currentPosition).normalized * 0.5f), (playerPosition - currentPosition).normalized * 50, Color.red);
    }

    private void FixedUpdate()
    {
        rigiBoy.MovePosition(rigiBoy.position + velocity * Time.fixedDeltaTime);
    }


}
