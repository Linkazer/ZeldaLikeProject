using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementFront : MonoBehaviour {

    public Transform player;
    public float speed;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -6)
        {
            transform.position = new Vector3(startPosition.x + player.position.x,startPosition.y + player.position.y, 0) * -1;
        }
        else
        {
            transform.position = new Vector3(-(startPosition.x+player.position.x), transform.position.y, 0);
        }
    }

}
